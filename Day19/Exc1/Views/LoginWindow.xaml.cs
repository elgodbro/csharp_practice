using System.Windows;
using Exc1.Models;
using Exc1.Services;

namespace Exc1.Views;

public partial class LoginWindow : Window
{
    private readonly DataStorage _dataStorage;

    public LoginWindow()
    {
        InitializeComponent();
        _dataStorage = new DataStorage();
        LoginTextBox.Focus();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordBox.Password;

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Введите логин и пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var users = _dataStorage.LoadUsers();
        var user = users.FirstOrDefault(u =>
            u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)
            && DataStorage.VerifyPassword(password, u.PasswordHash));

        if (user != null)
        {
            var mainWindow = new MainWindow(user);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            PasswordBox.Clear();
            PasswordBox.Focus();
        }
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordBox.Password;

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Введите логин и пароль для регистрации", "Ошибка регистрации", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }


        if (password.Length < 4)
        {
            MessageBox.Show("Пароль должен быть не менее 4 символов", "Ошибка регистрации", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var users = _dataStorage.LoadUsers();
        if (users.Any(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка регистрации", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var newUser = new User
        {
            Login = login,
            PasswordHash = DataStorage.HashPassword(password),
            Role = "User"
        };

        users.Add(newUser);
        _dataStorage.SaveUsers(users);

        MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти", "Регистрация успешна",
            MessageBoxButton.OK, MessageBoxImage.Information);
        PasswordBox.Clear();
        LoginTextBox.Focus();
    }
}