using System.Windows;
using Exc1.Models;
using Exc1.Services;
using Exc1.ViewModels;

namespace Exc1.Views;

public partial class MainWindow : Window
{
    private readonly User _currentUser;
    public User CurrentUser => _currentUser;

    public MainWindow(User currentUser)
    {
        InitializeComponent();
        _currentUser = currentUser;
        DataContext = new FinanceViewModel(_currentUser);
    }

    private void OpenMessagingWindow_Click(object sender, RoutedEventArgs e)
    {
        var messagingWindow = new MessagingWindow(_currentUser);
        messagingWindow.Show();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is FinanceViewModel vm)
            vm.Cleanup();
        var loginWindow = new LoginWindow();
        Application.Current.MainWindow = loginWindow;
        loginWindow.Show();
        Close();
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        if (DataContext is FinanceViewModel vm)
            vm.Cleanup();
        base.OnClosing(e);
    }
}