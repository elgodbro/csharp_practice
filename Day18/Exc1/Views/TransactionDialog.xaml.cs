using System.Windows;
using Exc1.Models;

namespace Exc1.Views;

public partial class TransactionDialog : Window
{
    public TransactionDialog()
    {
        InitializeComponent();
        DataContext = this;
    }

    public TransactionModel Transaction { get; set; }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Transaction.Type))
        {
            MessageBox.Show("Выберите тип транзакции", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

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