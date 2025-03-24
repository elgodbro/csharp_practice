namespace Exc4;

public class WarehouseMonitor
{
    public event EventHandler<WarehouseEventArgs> ItemMoved;

    public void MoveItem(string itemId, string fromLocation, string toLocation, string employeeId)
    {
        Console.WriteLine($"Предмет {itemId} перемещен из {fromLocation} в {toLocation}. Работик - {employeeId}");
        
        var eventArgs = new WarehouseEventArgs(itemId, fromLocation, toLocation, employeeId);
        OnItemMoved(eventArgs);
    }

    private void OnItemMoved(WarehouseEventArgs e)
    {
        ItemMoved?.Invoke(this, e);
    }
}