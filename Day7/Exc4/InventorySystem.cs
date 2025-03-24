namespace Exc4;

public class InventorySystem
{
    public void UpdateInventoryLocation(object sender, WarehouseEventArgs e)
    {
        Console.WriteLine($"[ХРАНИЛИЩЕ] Предмет \"{e.ItemId}\"  перемещен из {e.FromLocation} в {e.ToLocation}");
    }
}