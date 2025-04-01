using System;
using System.Windows;

namespace Exc1
{
    public partial class TransactionDialog : Window
    {
        public Transaction Transaction { get; set; } = new Transaction { Date = DateTime.Now };
        
        public TransactionDialog(string title)
        {
            InitializeComponent();
            Title = title;
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Transaction.Amount <= 0)
            {
                MessageBox.Show("Сумма должна быть больше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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