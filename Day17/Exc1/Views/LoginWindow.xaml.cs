using System.Linq;
using System.Windows;
using Exc1.Models;
using Exc1.Services;

namespace Exc1.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordBox.Password;
        var dataStorage = new DataStorage();
        var users = dataStorage.LoadUsers();
        var user = users.FirstOrDefault(u => u.Login == login && DataStorage.VerifyPassword(password, u.PasswordHash));
        if (user != null)
        {
            var mainWindow = new MainWindow(user);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordBox.Password;
        var dataStorage = new DataStorage();
        var users = dataStorage.LoadUsers();
        if (users.Any(u => u.Login == login))
        {
            MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        var newUser = new User { Login = login, PasswordHash = DataStorage.HashPassword(password), Role = "User" };
        users.Add(newUser);
        dataStorage.SaveUsers(users);
        MessageBox.Show("Регистрация успешна", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}