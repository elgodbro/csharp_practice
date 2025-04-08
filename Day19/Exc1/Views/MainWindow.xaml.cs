using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Exc1.Models;
using Exc1.ViewModels;

namespace Exc1.Views;

public partial class MainWindow : Window
{
    private readonly FinanceViewModel _viewModel;

    public MainWindow(User currentUser)
    {
        InitializeComponent();
        _viewModel = new FinanceViewModel(currentUser);
        DataContext = _viewModel;
    }

    private void OpenMessagingWindow_Click(object sender, RoutedEventArgs e)
    {
        var messagingWindow = new MessagingWindow(_viewModel.CurrentUser)
        {
            Owner = this
        };
        messagingWindow.Show();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        CleanupViewModel();

        var loginWindow = new LoginWindow();
        Application.Current.MainWindow = loginWindow;
        loginWindow.Show();
        Close();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        CleanupViewModel();
    }

    private void CleanupViewModel()
    {
        if (_viewModel is IDisposable disposableViewModel) disposableViewModel.Dispose();
    }

    private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not DataGrid grid || grid.SelectedItem == null) return;
        if (_viewModel.EditTransactionCommand.CanExecute(grid.SelectedItem))
            _viewModel.EditTransactionCommand.Execute(grid.SelectedItem);
    }
}