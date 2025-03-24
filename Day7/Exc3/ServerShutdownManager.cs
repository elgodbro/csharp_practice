namespace Exc3;

public class ServerShutdownManager
{
    public event ServerShutdownEventHandler ServerShuttingDown;
    
    public void InitiateShutdown(string reason)
    {
        Console.WriteLine($"Завершение работы сервера. {reason}");
        
        var shutdownTime = DateTime.Now.AddMinutes(2);
        var eventArgs = new ServerShutdownEventArgs(shutdownTime, reason);
        
        OnServerShuttingDown(eventArgs);
        
        Console.WriteLine($"Сервер будет выключен в {shutdownTime}");
    }

    private void OnServerShuttingDown(ServerShutdownEventArgs e)
    {
        ServerShuttingDown?.Invoke(this, e);
    }
}