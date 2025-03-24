namespace Exc4;

public class WarehouseEventArgs(string itemId, string fromLocation, string toLocation, string employeeId)
{
    public string ItemId { get; set; } = itemId;
    public string FromLocation { get; set; } = fromLocation;
    public string ToLocation { get; set; } = toLocation;
    public string EmployeeId { get; set; } = employeeId;
    public DateTime Timestamp { get; set; } = DateTime.Now;
}