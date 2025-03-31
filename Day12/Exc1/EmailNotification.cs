namespace Exc1;

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"[Email]: {message}");
    }
}