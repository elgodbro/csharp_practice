using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Exc1.Models;
using Exc1.Services;
using Exc1.Views;
using LiveCharts;
using LiveCharts.Wpf;

namespace Exc1.ViewModels;

public class FinanceViewModel : INotifyPropertyChanged
{
    private readonly FinanceService _financeService = new();

    private double _balance;
    private DateTime? _filterEndDate;

    private DateTime? _filterStartDate;

    private TransactionModel _selectedTransaction;

    public FinanceViewModel()
    {
        AddTransactionCommand = new RelayCommand(AddTransaction);
        EditTransactionCommand = new RelayCommand(EditTransaction, CanModifyTransaction);
        DeleteTransactionCommand = new RelayCommand(DeleteTransaction, CanModifyTransaction);
        FilterCommand = new RelayCommand(FilterTransactions);
        RefreshCommand = new RelayCommand(async _ => await LoadDataAsync());

        Task.Run(async () => await LoadDataAsync());
    }

    public ObservableCollection<TransactionModel> Transactions { get; set; } = new();

    public ObservableCollection<TransactionModel> Incomes =>
        new(Transactions.Where(t => t.Type == "Доход" && FilterByDate(t)));

    public ObservableCollection<TransactionModel> Expenses =>
        new(Transactions.Where(t => t.Type == "Расход" && FilterByDate(t)));

    public DateTime? FilterStartDate
    {
        get => _filterStartDate;
        set
        {
            _filterStartDate = value;
            OnPropertyChanged(nameof(FilterStartDate));
        }
    }

    public DateTime? FilterEndDate
    {
        get => _filterEndDate;
        set
        {
            _filterEndDate = value;
            OnPropertyChanged(nameof(FilterEndDate));
        }
    }

    public double Balance
    {
        get => _balance;
        set
        {
            _balance = value;
            OnPropertyChanged(nameof(Balance));
        }
    }

    public TransactionModel SelectedTransaction
    {
        get => _selectedTransaction;
        set
        {
            _selectedTransaction = value;
            OnPropertyChanged(nameof(SelectedTransaction));
        }
    }

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
        var loadedTransactions = await _financeService.LoadTransactionsAsync();
        App.Current.Dispatcher.Invoke(() =>
        {
            Transactions.Clear();
            foreach (var t in loadedTransactions)
                Transactions.Add(t);
            UpdateBalance();
            OnPropertyChanged(nameof(Incomes));
            OnPropertyChanged(nameof(Expenses));
            OnPropertyChanged(nameof(ExpenseSeries));
        });
    }

    private bool FilterByDate(TransactionModel transaction)
    {
        var afterStart = !FilterStartDate.HasValue || transaction.Date >= FilterStartDate.Value;
        var beforeEnd = !FilterEndDate.HasValue || transaction.Date <= FilterEndDate.Value;
        return afterStart && beforeEnd;
    }

    private void AddTransaction(object parameter)
    {
        var newTransaction = ShowTransactionDialog(null);
        if (newTransaction != null)
        {
            Transactions.Add(newTransaction);
            UpdateBalance();
            OnPropertyChanged(nameof(Incomes));
            OnPropertyChanged(nameof(Expenses));
            OnPropertyChanged(nameof(ExpenseSeries));
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
                UpdateBalance();
                OnPropertyChanged(nameof(Incomes));
                OnPropertyChanged(nameof(Expenses));
                OnPropertyChanged(nameof(ExpenseSeries));
            }
        }
    }

    private void DeleteTransaction(object parameter)
    {
        if (parameter is TransactionModel transaction)
        {
            Transactions.Remove(transaction);
            UpdateBalance();
            OnPropertyChanged(nameof(Incomes));
            OnPropertyChanged(nameof(Expenses));
            OnPropertyChanged(nameof(ExpenseSeries));
        }
    }

    private void FilterTransactions(object parameter)
    {
        UpdateBalance();
        OnPropertyChanged(nameof(Incomes));
        OnPropertyChanged(nameof(Expenses));
        OnPropertyChanged(nameof(ExpenseSeries));
    }

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
            dialog.Transaction = new TransactionModel
            {
                Date = DateTime.Now,
                Category = "Без категории",
                Amount = 0,
                Type = "Доход"
            };
        }
        else
        {
            dialog.Title = "Редактирование транзакции";
            dialog.Transaction = new TransactionModel
            {
                Date = transaction.Date,
                Category = transaction.Category,
                Amount = transaction.Amount,
                Type = transaction.Type
            };
        }

        if (dialog.ShowDialog() == true)
            return dialog.Transaction;
        return null;
    }

    private bool CanModifyTransaction(object parameter)
    {
        return parameter is TransactionModel;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}