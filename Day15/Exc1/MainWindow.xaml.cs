using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace Exc1 {
    public partial class MainWindow : Window, INotifyPropertyChanged {
        public ObservableCollection<Transaction> Incomes { get; set; } = new ObservableCollection<Transaction>();
        public ObservableCollection<Transaction> Expenses { get; set; } = new ObservableCollection<Transaction>();
        public ICommand AddTransactionCommand { get; }
        public ICommand AddIncomeCommand { get; }
        public ICommand AddExpenseCommand { get; }
        public ICommand EditTransactionCommand { get; }
        public ICommand DeleteTransactionCommand { get; }
        public List<string> ExpenseLabels { get; set; } = new List<string>();
        public ChartValues<double> ExpenseValues { get; set; } = new ChartValues<double>();

        private double _balance;
        public double Balance { get => _balance; set { _balance = value; OnPropertyChanged(nameof(Balance)); } }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            AddTransactionCommand = new RelayCommand(AddTransaction);
            AddIncomeCommand = new RelayCommand(AddIncome);
            AddExpenseCommand = new RelayCommand(AddExpense);
            EditTransactionCommand = new RelayCommand(EditTransaction, CanEditOrDelete);
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction, CanEditOrDelete);
            UpdateAll();
        }

        private Transaction GetSelectedTransaction() =>
            IncomesTab.IsSelected && IncomesGrid.SelectedItem != null ? (Transaction)IncomesGrid.SelectedItem :
            ExpensesTab.IsSelected && ExpensesGrid.SelectedItem != null ? (Transaction)ExpensesGrid.SelectedItem : null;
        
        private void UpdateAll() {
            IncomesGrid.ItemsSource = Incomes;
            ExpensesGrid.ItemsSource = Expenses;
            IncomesGrid.Items.Refresh();
            ExpensesGrid.Items.Refresh();
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            UpdateBalance();
            UpdateChart();
        }

        private void AddTransaction(object parameter) {
            var dialog = new TransactionDialog("Добавление транзакции");
            if (dialog.ShowDialog() != true) return;
            if (dialog.Transaction.Type == "Доход") Incomes.Add(dialog.Transaction);
            else Expenses.Add(dialog.Transaction);
            UpdateAll();
        }

        private void AddIncome(object parameter) {
            var dialog = new TransactionDialog("Добавление дохода") { Transaction = { Type = "Доход" } };
            if (dialog.ShowDialog() != true) return;
            Incomes.Add(dialog.Transaction);
            UpdateAll();
        }

        private void AddExpense(object parameter) {
            var dialog = new TransactionDialog("Добавление расхода") { Transaction = { Type = "Расход" } };
            if (dialog.ShowDialog() != true) return;
            Expenses.Add(dialog.Transaction);
            UpdateAll();
        }

        private void EditTransaction(object parameter) {
            var selected = GetSelectedTransaction();
            if (selected == null) return;
            var wasIncome = Incomes.Contains(selected);
            var dialog = new TransactionDialog("Редактирование транзакции", selected);
            if (dialog.ShowDialog() != true) return;
            selected.Date = dialog.Transaction.Date;
            selected.Category = dialog.Transaction.Category;
            selected.Amount = dialog.Transaction.Amount;
            selected.Type = dialog.Transaction.Type;
            if (selected.Type == "Доход" && !wasIncome) { Expenses.Remove(selected); Incomes.Add(selected); }
            else if (selected.Type == "Расход" && wasIncome) { Incomes.Remove(selected); Expenses.Add(selected); }
            UpdateAll();
        }

        private void DeleteTransaction(object parameter) {
            var selected = GetSelectedTransaction();
            if (selected == null) return;
            if (MessageBox.Show("Вы уверены, что хотите удалить эту транзакцию?", "Подтверждение", MessageBoxButton.YesNo)
                    != MessageBoxResult.Yes) return;
            if (!Incomes.Remove(selected)) Expenses.Remove(selected);
            IncomesGrid.SelectedItem = null;
            ExpensesGrid.SelectedItem = null;
            UpdateAll();
        }

        private void FilterTransactions_Click(object sender, RoutedEventArgs e) {
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;
            if (startDate.HasValue && endDate.HasValue) {
                var filteredIncomes = Incomes.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
                var filteredExpenses = Expenses.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
                IncomesGrid.ItemsSource = filteredIncomes;
                ExpensesGrid.ItemsSource = filteredExpenses;
                UpdateBalance(filteredIncomes, filteredExpenses);
                UpdateChart(filteredExpenses);
            }
            else UpdateAll();
        }

        private void UpdateBalance(List<Transaction> incomes = null, List<Transaction> expenses = null) {
            var totalIncomes = incomes?.Sum(t => t.Amount) ?? Incomes.Sum(t => t.Amount);
            var totalExpenses = expenses?.Sum(t => t.Amount) ?? Expenses.Sum(t => t.Amount);
            Balance = totalIncomes - totalExpenses;
            BalanceText.Foreground = Balance >= 0 ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }

        public SeriesCollection ExpenseSeries { get; set; } = new SeriesCollection();
        private static readonly List<string> Colors = new() { "#FF5733", "#33FF57", "#3357FF", "#FF33A1", "#A133FF" };

        private void UpdateChart(List<Transaction> transactions = null)
        {
            ExpenseSeries.Clear();
    
            var source = transactions?.ToList() ?? Expenses.ToList();
            var groupedExpenses = source
                .GroupBy(e => e.Category)
                .Select((g, index) => new
                {
                    Category = g.Key,
                    Amount = g.Sum(e => e.Amount),
                    Color = (SolidColorBrush)(new BrushConverter().ConvertFrom(Colors[index % Colors.Count]))
                })
                .ToList();

            foreach (var item in groupedExpenses)
            {
                ExpenseSeries.Add(new ColumnSeries
                {
                    Title = item.Category,
                    Values = new ChartValues<double> { item.Amount },
                    Fill = item.Color
                });
            }
        }

        
        private bool CanEditOrDelete(object parameter)
        {
            return IncomesGrid.SelectedItem != null || ExpensesGrid.SelectedItem != null;
        }
    }
    

    public class Transaction : INotifyPropertyChanged {
        public string Type { get; set; }
        private double _amount;
        public double Amount { get => _amount; set { _amount = value; OnPropertyChanged(nameof(Amount)); } }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        : ICommand
    {
        private readonly Action<object> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}
