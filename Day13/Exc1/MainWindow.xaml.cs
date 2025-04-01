using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace Exc1
{
    public partial class MainWindow : Window
    {
        public List<Transaction> Incomes { get; set; } = [];
        public List<Transaction> Expenses { get; set; } = [];
        
        public List<string> ExpenseLabels { get; set; }
        public ChartValues<double> ExpenseValues { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            ExpenseLabels = [];
            ExpenseValues = [];
            
            IncomesGrid.ItemsSource = Incomes;
            ExpensesGrid.ItemsSource = Expenses;
        }


        private void UpdateBalance()
        {
            var totalIncomes = Incomes.Sum(t => t.Amount);
            var totalExpenses = Expenses.Sum(t => t.Amount);
            var balance = totalIncomes - totalExpenses;
            
            BalanceText.Text = balance.ToString("N2") + " руб.";
            
            BalanceText.Foreground = balance >= 0 ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
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

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TransactionDialog("Добавление дохода");
            if (dialog.ShowDialog() == true)
            {
                Incomes.Add(dialog.Transaction);
                IncomesGrid.Items.Refresh();
                UpdateBalance();
            }
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TransactionDialog("Добавление расхода");
            if (dialog.ShowDialog() == true)
            {
                Expenses.Add(dialog.Transaction);
                ExpensesGrid.Items.Refresh();
                UpdateBalance();
                UpdateChart();
            }
        }
    }

    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
    }
}