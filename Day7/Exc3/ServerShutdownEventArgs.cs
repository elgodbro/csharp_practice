namespace Exc3;

public class ServerShutdownEventArgs(DateTime shutdownTime, string reason)
{
    public DateTime ShutdownTime { get; private set; } = shutdownTime;
    public string Reason { get; private set; } = reason;
}