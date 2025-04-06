using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Windows.Input;
using System.Windows.Media;
using Exc1.Models;
using Exc1.Services;
using Exc1.Views;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;

namespace Exc1.ViewModels;

public class FinanceViewModel : INotifyPropertyChanged
{
    private readonly FinanceService _financeService = new();
    private readonly MessageService _messageService = new();
    private List<TransactionModel> _allTransactions;
    private double _balance;
    private readonly double _budgetLimit = 0;
    private DateTime? _filterStartDate;
    private DateTime? _filterEndDate;
    private TransactionModel _selectedTransaction;
    private User _currentUser;
    private bool _isAdmin;
    public bool IsAdmin => _isAdmin;
    private readonly System.Timers.Timer _timer;

    public FinanceViewModel(User currentUser)
    {
        CurrentUser = currentUser;
        _isAdmin = currentUser.Role == "Admin";
        AddTransactionCommand = new RelayCommand(AddTransaction);
        EditTransactionCommand = new RelayCommand(EditTransaction, CanModifyTransaction);
        DeleteTransactionCommand = new RelayCommand(DeleteTransaction, CanModifyTransaction);
        FilterCommand = new RelayCommand(FilterTransactions);
        RefreshCommand = new RelayCommand(async _ => await LoadDataAsync());
        Transactions = new ObservableCollection<TransactionModel>();
        
        _selectedMonth = DateTime.Now;
        PreviousMonthCommand = new RelayCommand(_ => SelectedMonth = SelectedMonth.AddMonths(-1));
        NextMonthCommand = new RelayCommand(_ => SelectedMonth = SelectedMonth.AddMonths(1));

        _messageService.StartListening(CurrentUser.Login);
        _timer = new System.Timers.Timer(300000);
        _timer.Elapsed += (s, e) => UpdatePaymentsMmf();
        _timer.Start();

        Task.Run(async () => await LoadDataAsync());
    }

    public User CurrentUser
    {
        get => _currentUser;
        set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
    }

    public ObservableCollection<TransactionModel> Transactions { get; }

    public ObservableCollection<TransactionModel> Incomes =>
        new(Transactions.Where(t => t.Type == "Доход" && FilterByDate(t)));

    public ObservableCollection<TransactionModel> Expenses =>
        new(Transactions.Where(t => t.Type == "Расход" && FilterByDate(t)));

    public DateTime? FilterStartDate
    {
        get => _filterStartDate;
        set { _filterStartDate = value; OnPropertyChanged(nameof(FilterStartDate)); }
    }

    public DateTime? FilterEndDate
    {
        get => _filterEndDate;
        set { _filterEndDate = value; OnPropertyChanged(nameof(FilterEndDate)); }
    }

    public double Balance
    {
        get => _balance;
        set { _balance = value; OnPropertyChanged(nameof(Balance)); OnPropertyChanged(nameof(IsBudgetExceeded)); }
    }

    public TransactionModel SelectedTransaction
    {
        get => _selectedTransaction;
        set { _selectedTransaction = value; OnPropertyChanged(nameof(SelectedTransaction)); }
    }
    
    private DateTime _selectedMonth;

    public DateTime SelectedMonth
    {
        get => _selectedMonth;
        set
        {
            _selectedMonth = value;
            FilterStartDate = new DateTime(_selectedMonth.Year, _selectedMonth.Month, 1);
            FilterEndDate = FilterStartDate.Value.AddMonths(1).AddDays(-1);
            OnPropertyChanged(nameof(SelectedMonth));
            FilterTransactions(null);
        }
    }

    public ICommand PreviousMonthCommand { get; }
    public ICommand NextMonthCommand { get; }

    public ICommand AddTransactionCommand { get; }
    public ICommand EditTransactionCommand { get; }
    public ICommand DeleteTransactionCommand { get; }
    public ICommand FilterCommand { get; }
    public ICommand RefreshCommand { get; }

    public SeriesCollection ExpenseSeries
    {
        get
        {
            var series = new SeriesCollection();
            var grouped = Expenses.GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Amount = g.Sum(t => t.Amount) });
            var colors = new List<string> { "#FF5733", "#33FF57", "#3357FF", "#FF33A1", "#A133FF" };
            var i = 0;
            foreach (var group in grouped)
            {
                series.Add(new ColumnSeries
                {
                    Title = group.Category,
                    Values = new ChartValues<double> { group.Amount },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(colors[i % colors.Count])
                });
                i++;
            }
            return series;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public async Task LoadDataAsync()
    {
        _allTransactions = await _financeService.LoadTransactionsAsync();
        if (_isAdmin)
        {
            foreach (var transaction in _allTransactions)
            {
                transaction.UserName = transaction.UserId;
            }
        }
        UpdateTransactions();
        UpdateBalance();
        OnPropertyChanged(nameof(Incomes));
        OnPropertyChanged(nameof(Expenses));
        OnPropertyChanged(nameof(ExpenseSeries));
        UpdatePaymentsMmf();
    }

    private void UpdateTransactions()
    {
        Transactions.Clear();
        var filtered = CurrentUser.Role == "Admin" ? _allTransactions : _allTransactions.Where(t => t.UserId == CurrentUser.Login);
        foreach (var t in filtered)
            Transactions.Add(t);
    }

    private void AddTransaction(object parameter)
    {
        var newTransaction = ShowTransactionDialog(null);
        if (newTransaction != null)
        {
            newTransaction.UserId = CurrentUser.Login;
            _allTransactions.Add(newTransaction);
            if (CurrentUser.Role == "Admin" || newTransaction.UserId == CurrentUser.Login)
                Transactions.Add(newTransaction);
            _financeService.SaveTransactions(_allTransactions);
            UpdateBalance();
            OnPropertyChanged(nameof(Incomes));
            OnPropertyChanged(nameof(Expenses));
            OnPropertyChanged(nameof(ExpenseSeries));
            UpdatePaymentsMmf();
        }
    }

    private void EditTransaction(object parameter)
    {
        if (parameter is TransactionModel transaction)
        {
            var updatedTransaction = ShowTransactionDialog(transaction);
            if (updatedTransaction != null)
            {
                transaction.Date = updatedTransaction.Date;
                transaction.Category = updatedTransaction.Category;
                transaction.Amount = updatedTransaction.Amount;
                transaction.Type = updatedTransaction.Type;
                transaction.DueDate = updatedTransaction.DueDate;
                _financeService.SaveTransactions(_allTransactions);
                UpdateBalance();
                OnPropertyChanged(nameof(Incomes));
                OnPropertyChanged(nameof(Expenses));
                OnPropertyChanged(nameof(ExpenseSeries));
                UpdatePaymentsMmf();
            }
        }
    }

    private void DeleteTransaction(object parameter)
    {
        if (parameter is TransactionModel transaction)
        {
            _allTransactions.Remove(transaction);
            Transactions.Remove(transaction);
            _financeService.SaveTransactions(_allTransactions);
            UpdateBalance();
            OnPropertyChanged(nameof(Incomes));
            OnPropertyChanged(nameof(Expenses));
            OnPropertyChanged(nameof(ExpenseSeries));
            UpdatePaymentsMmf();
        }
    }

    private void FilterTransactions(object parameter)
    {
        UpdateBalance();
        OnPropertyChanged(nameof(Incomes));
        OnPropertyChanged(nameof(Expenses));
        OnPropertyChanged(nameof(ExpenseSeries));
    }

    public bool IsBudgetExceeded => _balance < _budgetLimit;
    
    private void UpdateBalance()
    {
        var totalIncome = Transactions.Where(t => t.Type == "Доход" && FilterByDate(t)).Sum(t => t.Amount);
        var totalExpense = Transactions.Where(t => t.Type == "Расход" && FilterByDate(t)).Sum(t => t.Amount);
        Balance = totalIncome - totalExpense;
    }

    private TransactionModel ShowTransactionDialog(TransactionModel transaction)
    {
        var dialog = new TransactionDialog();
        if (transaction == null)
        {
            dialog.Title = "Добавление транзакции";
            dialog.Transaction = new TransactionModel { Date = DateTime.Now, Category = "Без категории", Amount = 0, Type = "Доход" };
        }
        else
        {
            dialog.Title = "Редактирование транзакции";
            dialog.Transaction = new TransactionModel
            {
                Date = transaction.Date,
                Category = transaction.Category,
                Amount = transaction.Amount,
                Type = transaction.Type,
                DueDate = transaction.DueDate,
                UserId = transaction.UserId
            };
        }
        return dialog.ShowDialog() == true ? dialog.Transaction : null;
    }

    private bool FilterByDate(TransactionModel transaction)
    {
        var afterStart = !FilterStartDate.HasValue || transaction.Date >= FilterStartDate.Value;
        var beforeEnd = !FilterEndDate.HasValue || transaction.Date <= FilterEndDate.Value;
        return afterStart && beforeEnd;
    }

    private bool CanModifyTransaction(object parameter) => parameter is TransactionModel;

    private void UpdatePaymentsMmf()
    {
        var upcomingPayments = _allTransactions.Where(t => t.DueDate.HasValue && t.DueDate > DateTime.Now).ToList();
        var json = JsonConvert.SerializeObject(upcomingPayments);
        using (var mmf = MemoryMappedFile.CreateOrOpen("FinanceApp_Payments", 1024 * 1024))
        {
            using (var accessor = mmf.CreateViewAccessor())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(json);
                accessor.WriteArray(0, bytes, 0, bytes.Length);
            }
        }
    }

    public void Cleanup()
    {
        _messageService.StopListening();
        _timer.Stop();
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}