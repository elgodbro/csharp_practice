using Exc4;

var warehouseMonitor = new WarehouseMonitor();
var inventoryTracker = new InventoryTracker(warehouseMonitor);

warehouseMonitor.MoveItem("Книга", "Полка-A1", "Полка-B3", "E1234");
Console.WriteLine();
warehouseMonitor.MoveItem("Телевизор", "Склад-01", "Склад-02", "V5678");