namespace Exc3;

public class AlertSystem
{
    public void NotifyAdmin(object sender, ServerShutdownEventArgs e)
    {
        Console.WriteLine($"ОПОВЕЩЕНИЕ: Время выключения: {e.ShutdownTime}");
        Console.WriteLine($"ОПОВЕЩЕНИЕ: Причина: {e.Reason}");
    }
}