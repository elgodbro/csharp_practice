namespace Exc1;

public class Logger
{
    
    private readonly Stack<LogEntry> _logStack = new();
    
    public void AddLog(string message)
    {
        var entry = new LogEntry(DateTime.Now, message);
        _logStack.Push(entry);
    }
    
    public LogEntry RemoveLog() => _logStack.Count > 0 ? _logStack.Pop() : null!;
    
    public IEnumerable<LogEntry> Search(string keyword) => _logStack.Where(log => log.Message.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    
    public IEnumerable<LogEntry> GetSortedLogs() => _logStack.OrderBy(log => log.Timestamp);
    
    public IEnumerable<LogEntry> FilterByDate(DateTime date) => _logStack.Where(log => log.Timestamp.Date == date.Date);
    
    public void PrintLogs()
    {
        foreach (var log in _logStack)
        {
            Console.WriteLine(log);
        }
    }
}