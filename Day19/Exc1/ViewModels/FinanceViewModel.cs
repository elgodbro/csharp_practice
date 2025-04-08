using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Exc1.Models;
using Exc1.Persistence;
using Exc1.Services;
using Exc1.Views;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace Exc1.ViewModels;

public class FinanceViewModel : INotifyPropertyChanged, IDisposable
{
    private const double BudgetLimit = 0;
    private readonly FinanceDbContext _context;
    private readonly MessageService _messageService = new();
    private readonly string _mmfName;
    private readonly Timer _mmfUpdateTimer;
    private readonly ITransactionRepository _transactionRepository;
    private List<TransactionModel> _allTransactions = new();
    private double _balance;
    private Category _defaultCategory;
    private bool _disposed;
    private SeriesCollection _expenseSeries;
    private DateTime? _filterEndDate;
    private DateTime? _filterStartDate;
    private bool _isLoading;
    private DateTime _selectedMonth;
    private TransactionModel _selectedTransaction;

    public FinanceViewModel(User currentUser)
    {
        CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        IsAdmin = CurrentUser.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;

        _mmfName = $"FinanceApp_Payments_{CurrentUser.Login}";
        _context = new FinanceDbContext();
        _transactionRepository = new TransactionRepository(_context);

        Transactions = new ObservableCollection<TransactionModel>();
        AvailableCategories = new ObservableCollection<Category>();

        AddTransactionCommand = new RelayCommand(async _ => await AddTransactionAsync(), _ => CanExecute());
        EditTransactionCommand = new RelayCommand(async _ => await EditTransactionAsync(), _ => CanModifyTransaction());
        DeleteTransactionCommand =
            new RelayCommand(async _ => await DeleteTransactionAsync(), _ => CanModifyTransaction());
        FilterCommand = new RelayCommand(_ => ApplyFiltersAndUpdateUI(), _ => CanExecute());
        RefreshCommand = new RelayCommand(async _ => await RefreshDataAsync(), _ => CanExecute());

        _selectedMonth = DateTime.Now;
        PreviousMonthCommand = new RelayCommand(_ => ChangeMonth(-1), _ => CanExecute());
        NextMonthCommand = new RelayCommand(_ => ChangeMonth(1), _ => CanExecute());

        _messageService.StartListening(CurrentUser.Login);
        _mmfUpdateTimer = new Timer(300000);
        _mmfUpdateTimer.Elapsed += (s, e) => UpdatePaymentsMmf();
        _mmfUpdateTimer.AutoReset = true;
        _mmfUpdateTimer.Start();

        Task.Run(async () => await LoadInitialDataAsync());
    }

    public ObservableCollection<TransactionModel> Transactions { get; }
    public ObservableCollection<Category> AvailableCategories { get; }
    public Func<double, string> FormatAsCurrency => value => value.ToString("N2");
    public User CurrentUser { get; }
    public bool IsAdmin { get; }

    public DateTime? FilterStartDate
    {
        get => _filterStartDate;
        set
        {
            if (SetProperty(ref _filterStartDate, value)) ApplyFiltersAndUpdateUI();
        }
    }

    public DateTime? FilterEndDate
    {
        get => _filterEndDate;
        set
        {
            if (SetProperty(ref _filterEndDate, value)) ApplyFiltersAndUpdateUI();
        }
    }

    public double Balance
    {
        get => _balance;
        private set
        {
            if (SetProperty(ref _balance, value)) OnPropertyChanged(nameof(IsBudgetExceeded));
        }
    }

    public TransactionModel SelectedTransaction
    {
        get => _selectedTransaction;
        set => SetProperty(ref _selectedTransaction, value);
    }

    public DateTime SelectedMonth
    {
        get => _selectedMonth;
        private set
        {
            if (_isLoading) return;

            if (!SetProperty(ref _selectedMonth, value)) return;
            var newStart = new DateTime(_selectedMonth.Year, _selectedMonth.Month, 1);
            var newEnd = newStart.AddMonths(1).AddDays(-1);
            var changed = false;
            if (_filterStartDate != newStart)
            {
                _filterStartDate = newStart;
                changed = true;
                OnPropertyChanged(nameof(FilterStartDate));
            }

            if (_filterEndDate != newEnd)
            {
                _filterEndDate = newEnd;
                changed = true;
                OnPropertyChanged(nameof(FilterEndDate));
            }

            if (changed) ApplyFiltersAndUpdateUI();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        private set
        {
            if (Application.Current?.Dispatcher.CheckAccess() ?? true)
            {
                if (SetProperty(ref _isLoading, value)) CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (SetProperty(ref _isLoading, value)) CommandManager.InvalidateRequerySuggested();
                });
            }
        }
    }

    public bool IsBudgetExceeded => Balance < BudgetLimit;

    public SeriesCollection ExpenseSeries
    {
        get => _expenseSeries;
        private set => SetProperty(ref _expenseSeries, value);
    }

    public ICommand AddTransactionCommand { get; }
    public ICommand EditTransactionCommand { get; }
    public ICommand DeleteTransactionCommand { get; }
    public ICommand FilterCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand PreviousMonthCommand { get; }
    public ICommand NextMonthCommand { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private bool CanExecute()
    {
        return !IsLoading;
    }

    private bool CanModifyTransaction()
    {
        return SelectedTransaction != null && !IsLoading;
    }

    private void ChangeMonth(int monthDelta)
    {
        if (IsLoading) return;

        SelectedMonth = SelectedMonth.AddMonths(monthDelta);
    }

    private async Task LoadInitialDataAsync()
    {
        IsLoading = true;
        try
        {
            await LoadCategoriesInternalAsync();
            await LoadDataInternalAsync();

            Application.Current?.Dispatcher.Invoke(() =>
            {
                ApplyFiltersAndUpdateUI();
                UpdatePaymentsMmf();
            });
        }
        catch (Exception ex)
        {
            Application.Current?.Dispatcher.Invoke(() =>
            {
                HandleError("Ошибка начальной загрузки", ex);
                _allTransactions.Clear();
                ApplyFiltersAndUpdateUI();
            });
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task RefreshDataAsync()
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            await LoadCategoriesInternalAsync();
            await LoadDataInternalAsync();

            Application.Current?.Dispatcher.Invoke(() =>
            {
                ApplyFiltersAndUpdateUI();
                UpdatePaymentsMmf();
            });
        }
        catch (Exception ex)
        {
            Application.Current?.Dispatcher.Invoke(() => { HandleError("Ошибка обновления данных", ex); });
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task LoadCategoriesInternalAsync()
    {
        var categories = await _transactionRepository.GetCategoriesAsync()
            .ConfigureAwait(false);

        _defaultCategory = categories.FirstOrDefault(c => c.Name == "Другой расход")
                           ?? categories.FirstOrDefault();

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            AvailableCategories.Clear();
            foreach (var cat in categories) AvailableCategories.Add(cat);
        });
    }

    private async Task LoadDataInternalAsync()
    {
        var loadedTransactions = await _transactionRepository.GetTransactionsAsync(CurrentUser.Login, IsAdmin)
            .ConfigureAwait(false);

        if (IsAdmin)
            foreach (var transaction in loadedTransactions)
                transaction.UserName = transaction.UserId;

        _allTransactions = loadedTransactions;
    }


    private void ApplyFiltersAndUpdateUI()
    {
        if (!Application.Current.Dispatcher.CheckAccess())
        {
            Application.Current.Dispatcher.Invoke(ApplyFiltersAndUpdateUI);
            return;
        }

        if (_allTransactions == null) return;

        var startDate = FilterStartDate ?? new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
        var endDate = FilterEndDate ?? startDate.AddMonths(1).AddDays(-1);

        var filtered = _allTransactions
            .Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date)
            .ToList();

        Transactions.Clear();
        foreach (var t in filtered) Transactions.Add(t);

        UpdateBalance(filtered);
        UpdateChart(filtered);
    }

    private void UpdateBalance(List<TransactionModel> filteredTransactions)
    {
        Balance = filteredTransactions.Sum(t =>
            t.Type.Equals("Доход", StringComparison.OrdinalIgnoreCase) ? t.Amount : -t.Amount);
    }

    private void UpdateChart(List<TransactionModel> filteredTransactions)
    {
        var series = new SeriesCollection();
        var expenses = filteredTransactions
            .Where(t => t.Type.Equals("Расход", StringComparison.OrdinalIgnoreCase));
        var grouped = expenses
            .GroupBy(t => t.Category?.Name ?? "Без категории")
            .Select(g => new { CategoryName = g.Key, Amount = g.Sum(t => t.Amount) })
            .Where(g => g.Amount > 0)
            .OrderByDescending(g => g.Amount);

        var colors = new List<string>
            { "#FF5733", "#33FF57", "#3357FF", "#FF33A1", "#A133FF", "#FFBD33", "#33FFF3", "#F3FF33" };
        var i = 0;
        foreach (var group in grouped)
        {
            series.Add(new ColumnSeries
            {
                Title = group.CategoryName,
                Values = new ChartValues<double> { group.Amount },
                DataLabels = true,
                Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(colors[i % colors.Count])
            });
            i++;
        }

        ExpenseSeries = series;
    }

    private async Task AddTransactionAsync()
    {
        if (IsLoading) return;
        if (!AvailableCategories.Any())
        {
            MessageBox.Show("Категории еще не загружены. Пожалуйста, подождите или обновите.", "Внимание",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var initialCategoryId = _defaultCategory?.Id ?? AvailableCategories.First().Id;

        var dialog = new TransactionDialog
        {
            Title = "Добавление транзакции",
            Transaction = new TransactionModel
            {
                Date = DateTime.Now, Amount = 0, Type = "Расход",
                UserId = CurrentUser.Login, CategoryId = initialCategoryId
            },
            AvailableCategories = AvailableCategories,
            Owner = Application.Current?.MainWindow
        };

        if (dialog.ShowDialog() == true)
        {
            IsLoading = true;
            try
            {
                var newTransaction = dialog.Transaction;
                await _transactionRepository.AddTransactionAsync(newTransaction);
                await _transactionRepository.SaveChangesAsync();
                var savedTransaction = await _transactionRepository.GetTransactionByIdAsync(newTransaction.Id);
                if (savedTransaction != null)
                {
                    _allTransactions.Add(savedTransaction);
                    _allTransactions = _allTransactions.OrderByDescending(t => t.Date).ToList();
                    ApplyFiltersAndUpdateUI();
                    UpdatePaymentsMmf();
                    SelectedTransaction = savedTransaction;
                }
                else
                {
                    await RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                HandleError("Ошибка добавления транзакции", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private async Task EditTransactionAsync()
    {
        if (SelectedTransaction == null || IsLoading || !AvailableCategories.Any()) return;

        var transactionToEditCopy = new TransactionModel
        {
            Id = SelectedTransaction.Id, Date = SelectedTransaction.Date,
            Amount = SelectedTransaction.Amount, Type = SelectedTransaction.Type,
            DueDate = SelectedTransaction.DueDate, UserId = SelectedTransaction.UserId,
            CategoryId = SelectedTransaction.CategoryId
        };

        var dialog = new TransactionDialog
        {
            Title = "Редактирование транзакции",
            Transaction = transactionToEditCopy,
            AvailableCategories = AvailableCategories,
            Owner = Application.Current?.MainWindow
        };

        if (dialog.ShowDialog() == true)
        {
            IsLoading = true;
            try
            {
                var updatedTransaction = dialog.Transaction;
                await _transactionRepository.UpdateTransactionAsync(updatedTransaction);
                await _transactionRepository.SaveChangesAsync();

                var savedTransaction = await _transactionRepository.GetTransactionByIdAsync(updatedTransaction.Id);
                var index = _allTransactions.FindIndex(t => t.Id == updatedTransaction.Id);

                if (index != -1 && savedTransaction != null)
                {
                    _allTransactions[index] = savedTransaction;
                    _allTransactions = _allTransactions.OrderByDescending(t => t.Date).ToList();
                    ApplyFiltersAndUpdateUI();
                    UpdatePaymentsMmf();
                    SelectedTransaction = savedTransaction;
                }
                else
                {
                    await RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                HandleError("Ошибка редактирования транзакции", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private async Task DeleteTransactionAsync()
    {
        if (SelectedTransaction == null || IsLoading) return;

        var transactionToDelete = SelectedTransaction;
        var result = MessageBox.Show(
            $"Удалить транзакцию (Категория: {transactionToDelete.Category?.Name ?? "N/A"}, Сумма: {transactionToDelete.Amount:N2}, Дата: {transactionToDelete.Date:d})?",
            "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            IsLoading = true;
            try
            {
                await _transactionRepository.DeleteTransactionAsync(transactionToDelete.Id);
                await _transactionRepository.SaveChangesAsync();

                _allTransactions.RemoveAll(t => t.Id == transactionToDelete.Id);

                ApplyFiltersAndUpdateUI();
                UpdatePaymentsMmf();

                SelectedTransaction = null;
            }
            catch (Exception ex)
            {
                HandleError("Ошибка удаления транзакции", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private void UpdatePaymentsMmf()
    {
        if (_allTransactions == null || string.IsNullOrEmpty(_mmfName)) return;


        var upcoming = _allTransactions
            .Where(t => t.UserId == CurrentUser.Login &&
                        t.DueDate.HasValue &&
                        t.DueDate.Value.Date >= DateTime.Today)
            .OrderBy(t => t.DueDate)
            .ToList();

        try
        {
            var json = JsonConvert.SerializeObject(upcoming);
            var bytes = Encoding.UTF8.GetBytes(json);
            long requiredSize = Math.Max(1024, bytes.Length + 102);

            using var mmf = MemoryMappedFile.CreateOrOpen(_mmfName, requiredSize);
            using var accessor = mmf.CreateViewAccessor();
            if (bytes.Length > ushort.MaxValue) return;
            accessor.Write(0, (ushort)bytes.Length);
            accessor.WriteArray(2, bytes, 0, bytes.Length);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating MMF '{_mmfName}': {ex.Message}");
        }
    }

    private void HandleError(string contextMessage, Exception ex)
    {
        Debug.WriteLine($"{contextMessage}: {ex}");

        Action showError = () => MessageBox.Show($"{contextMessage}:\n{ex.InnerException?.Message ?? ex.Message}",
            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        if (Application.Current?.Dispatcher.CheckAccess() ?? true) showError();
        else Application.Current.Dispatcher.Invoke(showError);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _messageService?.StopListening();
            _mmfUpdateTimer?.Stop();
            _mmfUpdateTimer?.Dispose();
            _context?.Dispose();
        }

        _disposed = true;
    }
}