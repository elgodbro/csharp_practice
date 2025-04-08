using System.Windows;
using Exc1.Models;
using Exc1.Services;

namespace Exc1.Views;

public partial class MessagingWindow : Window
{
    private readonly User _currentUser;

    public MessagingWindow(User currentUser)
    {
        InitializeComponent();
        _currentUser = currentUser;
    }

    private void SendMessage_Click(object sender, RoutedEventArgs e)
    {
        var recipient = RecipientTextBox.Text;
        var message = MessageTextBox.Text;
        if (!string.IsNullOrWhiteSpace(recipient) && !string.IsNullOrWhiteSpace(message))
        {
            var messageService = new MessageService();
            messageService.SendMessage(recipient, $"{_currentUser.Login}: {message}");
            MessageTextBox.Text = string.Empty;
        }
    }
}