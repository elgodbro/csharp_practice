using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;

namespace Exc1
{
    public partial class MainWindow : Window
    {
        public List<Transaction> Incomes { get; set; } = [];
        public List<Transaction> Expenses { get; set; } = [];

        public ICommand AddTransactionCommand { get; }
        public ICommand AddIncomeCommand { get; }
        public ICommand AddExpenseCommand { get; }
        public ICommand EditTransactionCommand { get; }
        public ICommand DeleteTransactionCommand { get; }

        public List<string> ExpenseLabels { get; set; }
        public ChartValues<double> ExpenseValues { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            AddTransactionCommand = new RelayCommand(AddTransaction);
            AddIncomeCommand = new RelayCommand(AddIncome);
            AddExpenseCommand = new RelayCommand(AddExpense);
            EditTransactionCommand = new RelayCommand(EditTransaction, CanEditOrDelete);
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction, CanEditOrDelete);

            ExpenseLabels = [];
            ExpenseValues = [];
            
            IncomesGrid.ItemsSource = Incomes;
            ExpensesGrid.ItemsSource = Expenses;
        }

        private bool CanEditOrDelete(object parameter)
        {
            return IncomesGrid.SelectedItem != null || ExpensesGrid.SelectedItem != null;
        }

        private Transaction GetSelectedTransaction()
        {
            if (IncomesTab.IsSelected && IncomesGrid.SelectedItem != null)
                return (Transaction)IncomesGrid.SelectedItem;
            if (ExpensesTab.IsSelected && ExpensesGrid.SelectedItem != null)
                return (Transaction)ExpensesGrid.SelectedItem;
            return null;
        }

        private void AddTransaction(object parameter)
        {
            var dialog = new TransactionDialog("Добавление транзакции");
            if (dialog.ShowDialog() != true) return;
            if (dialog.Transaction.Type == "Доход")
                Incomes.Add(dialog.Transaction);
            else
                Expenses.Add(dialog.Transaction);
            RefreshGrids();
            UpdateBalance();
            UpdateChart();
        }

        private void AddIncome(object parameter)
        {
            var dialog = new TransactionDialog("Добавление дохода");
            dialog.Transaction.Type = "Доход";
            if (dialog.ShowDialog() != true) return;
            Incomes.Add(dialog.Transaction);
            IncomesGrid.Items.Refresh();
            UpdateBalance();
        }

        private void AddExpense(object parameter)
        {
            var dialog = new TransactionDialog("Добавление расхода");
            dialog.Transaction.Type = "Расход";
            if (dialog.ShowDialog() != true) return;
            Expenses.Add(dialog.Transaction);
            ExpensesGrid.Items.Refresh();
            UpdateBalance();
            UpdateChart();
        }

        private void EditTransaction(object parameter)
        {
            var selected = GetSelectedTransaction();
            if (selected == null) return;
            
            var wasIncome = Incomes.Contains(selected);
            var dialog = new TransactionDialog("Редактирование транзакции", selected);

            if (dialog.ShowDialog() != true) return;
            
            selected.Date = dialog.Transaction.Date;
            selected.Category = dialog.Transaction.Category;
            selected.Amount = dialog.Transaction.Amount;
            selected.Type = dialog.Transaction.Type;
            
            switch (selected.Type)
            {
                case "Доход" when !wasIncome:
                    Expenses.Remove(selected);
                    Incomes.Add(selected);
                    break;
                case "Расход" when wasIncome:
                    Incomes.Remove(selected);
                    Expenses.Add(selected);
                    break;
            }

            RefreshGrids();
            UpdateBalance();
            UpdateChart();
        }

        private void DeleteTransaction(object parameter)
        {
            var selected = GetSelectedTransaction();
            if (selected == null) return;
            if (MessageBox.Show("Вы уверены, что хотите удалить эту транзакцию?",
                    "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            if (!Incomes.Remove(selected))
                Expenses.Remove(selected);
            RefreshGrids();
            UpdateBalance();
            UpdateChart();
        }

        private void RefreshGrids()
        {
            IncomesGrid.Items.Refresh();
            ExpensesGrid.Items.Refresh();
        }
        
        private void UpdateBalance()
        {
            var totalIncomes = Incomes.Sum(t => t.Amount);
            var totalExpenses = Expenses.Sum(t => t.Amount);
            var balance = totalIncomes - totalExpenses;

            BalanceText.Text = balance.ToString("N2") + " руб.";

            BalanceText.Foreground =
                balance >= 0 ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }

        private void UpdateChart()
        {
            var expensesByCategory = Expenses
                .GroupBy(e => e.Category)
                .Select(g => new { Category = g.Key, Amount = g.Sum(t => t.Amount) })
                .OrderByDescending(x => x.Amount)
                .ToList();

            ExpenseLabels.Clear();
            ExpenseValues.Clear();

            foreach (var item in expensesByCategory)
            {
                ExpenseLabels.Add(item.Category);
                ExpenseValues.Add(item.Amount);
            }
        }
        
    }

    public class Transaction
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
    }
}