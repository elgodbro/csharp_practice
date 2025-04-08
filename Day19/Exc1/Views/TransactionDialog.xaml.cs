using System.Collections.ObjectModel;
using System.Windows;
using Exc1.Models;

namespace Exc1.Views;

public partial class TransactionDialog : Window
{
    public TransactionDialog()
    {
        InitializeComponent();
        DataContext = this;
        AvailableCategories = new ObservableCollection<Category>();
    }

    public TransactionModel Transaction { get; set; }
    public ObservableCollection<Category> AvailableCategories { get; set; }


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        CategoryComboBox.Focus();
    }


    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        OkButton.Focus();

        if (Transaction == null)
        {
            MessageBox.Show("Ошибка данных формы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(Transaction.Type))
        {
            MessageBox.Show("Выберите тип транзакции (Доход/Расход)", "Ошибка валидации",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (Transaction.Date == default)
        {
            MessageBox.Show("Выберите дату транзакции", "Ошибка валидации", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        if (Transaction.CategoryId <= 0)
        {
            MessageBox.Show("Выберите категорию из списка", "Ошибка валидации", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            CategoryComboBox.Focus();
            return;
        }

        if (Transaction.Amount <= 0)
        {
            MessageBox.Show("Сумма транзакции должна быть положительным числом", "Ошибка валидации",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            AmountTextBox.Focus();
            AmountTextBox.SelectAll();
            return;
        }

        Transaction.Amount = Math.Abs(Transaction.Amount);

        if (Transaction.DueDate.HasValue && Transaction.DueDate.Value.Date < Transaction.Date.Date)
        {
            MessageBox.Show("Дата платежа не может быть раньше даты транзакции", "Ошибка валидации",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}