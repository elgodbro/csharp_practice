using System;
using System.Windows;
using System.Windows.Controls;

namespace Exc1
{
    public partial class TransactionDialog : Window
    {
        public Transaction Transaction { get; set; }
        
        public bool IsIncome
        {
            get => Transaction.Type == "Доход";
            set { if (value) Transaction.Type = "Доход"; }
        }

        public bool IsExpense
        {
            get => Transaction.Type == "Расход";
            set { if (value) Transaction.Type = "Расход"; }
        }

        public TransactionDialog(string title, Transaction transaction = null)
        {
            InitializeComponent();
            Title = title;

            if (transaction != null)
                Transaction = transaction;
            else
                Transaction = new Transaction { Type = "Доход", Date = DateTime.Now };

            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Transaction.Type))
            {
                MessageBox.Show("Выберите тип транзакции", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Transaction.Amount <= 0)
            {
                MessageBox.Show("Сумма должна быть больше нуля", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Transaction.Category))
                Transaction.Category = "Без категории";
            
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}