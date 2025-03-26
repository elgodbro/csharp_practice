namespace Exc1;

public class LogEntry(DateTime timestamp, string message)
{
    public DateTime Timestamp { get; set; } = timestamp;
    public string Message { get; set; } = message;

    public override string ToString()
    {
        return $"{Timestamp:G}: {Message}";
    }
}