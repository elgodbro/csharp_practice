namespace Exc4;

public class InventoryTracker
{
    private readonly InventorySystem _inventorySystem;
    private readonly SecuritySystem _securitySystem;
    private readonly WarehouseMonitor _warehouseMonitor;

    public InventoryTracker(WarehouseMonitor warehouseMonitor)
    {
        _warehouseMonitor = warehouseMonitor;
        _inventorySystem = new InventorySystem();
        _securitySystem = new SecuritySystem();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _warehouseMonitor.ItemMoved += _inventorySystem.UpdateInventoryLocation;
        _warehouseMonitor.ItemMoved += _securitySystem.VerifyMovementAuthorization;
    }
}