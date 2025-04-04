using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Exc1.Services;

public class MessageService
{
    private CancellationTokenSource _cts;

    public void StartListening(string login)
    {
        _cts = new CancellationTokenSource();
        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                using (var server = new NamedPipeServerStream("FinanceApp_" + login, PipeDirection.In))
                {
                    await server.WaitForConnectionAsync(_cts.Token);
                    if (_cts.Token.IsCancellationRequested) break;
                    using (var reader = new StreamReader(server))
                    {
                        var message = await reader.ReadToEndAsync();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show(message, "Новое сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        });
                    }
                }
            }
        }, _cts.Token);
    }

    public void StopListening()
    {
        _cts?.Cancel();
    }

    public void SendMessage(string recipient, string message)
    {
        try
        {
            using (var client = new NamedPipeClientStream(".", "FinanceApp_" + recipient, PipeDirection.Out))
            {
                client.Connect(1000);
                using (var writer = new StreamWriter(client))
                {
                    writer.Write(message);
                    writer.Flush();
                }
            }
        }
        catch (TimeoutException)
        {
            MessageBox.Show("Получатель не в сети", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}