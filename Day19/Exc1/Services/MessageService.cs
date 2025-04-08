using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Windows;

namespace Exc1.Services;

public class MessageService
{
    private readonly string _pipeNamePrefix = "FinanceApp_Exc1_";
    private CancellationTokenSource _cts;

    public void StartListening(string login)
    {
        if (string.IsNullOrWhiteSpace(login)) return;

        StopListening();
        _cts = new CancellationTokenSource();
        var pipeName = _pipeNamePrefix + login;

        Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
                try
                {
                    await using var server = new NamedPipeServerStream(pipeName, PipeDirection.In, 1,
                        PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                    await server.WaitForConnectionAsync(_cts.Token);
                    if (_cts.Token.IsCancellationRequested) break;

                    using var reader = new StreamReader(server);
                    var message = await reader.ReadToEndAsync();

                    Application.Current?.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show(message, $"Сообщение для {login}", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    });
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (IOException ex)
                {
                    Debug.WriteLine($"Pipe Error (Server): {ex.Message}");
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unexpected Error (Server): {ex.Message}");
                    break;
                }

            Debug.WriteLine($"Listener stopped for {login}.");
        }, _cts.Token);
        Debug.WriteLine($"Listener started for {login}.");
    }

    public void StopListening()
    {
        if (_cts == null || _cts.IsCancellationRequested) return;
        Debug.WriteLine("Stopping listener...");
        _cts.Cancel();
        _cts.Dispose();
        _cts = null;
    }

    public void SendMessage(string recipientLogin, string message)
    {
        if (string.IsNullOrWhiteSpace(recipientLogin) || string.IsNullOrWhiteSpace(message))
        {
            MessageBox.Show("Получатель и сообщение не могут быть пустыми.", "Ошибка отправки", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var pipeName = _pipeNamePrefix + recipientLogin;
        try
        {
            using (var client = new NamedPipeClientStream(".", pipeName, PipeDirection.Out, PipeOptions.None))
            {
                client.Connect(1000);
                using (var writer = new StreamWriter(client))
                {
                    writer.Write(message);
                    writer.Flush();
                }
            }

            Debug.WriteLine($"Message sent to {recipientLogin}.");
        }
        catch (TimeoutException)
        {
            MessageBox.Show($"Получатель '{recipientLogin}' не в сети или не отвечает.", "Ошибка подключения",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (IOException ex)
        {
            MessageBox.Show($"Ошибка отправки сообщения: {ex.Message}", "Ошибка I/O", MessageBoxButton.OK,
                MessageBoxImage.Error);
            Debug.WriteLine($"Pipe Error (Client): {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Непредвиденная ошибка при отправке: {ex.Message}", "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            Debug.WriteLine($"Unexpected Error (Client): {ex.Message}");
        }
    }
}