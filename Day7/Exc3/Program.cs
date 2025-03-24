using Exc3;

var shutdownManager = new ServerShutdownManager();

var primaryBackup = new BackupService("Основной бэкап");
var secondaryBackup = new BackupService("Резервный бэкап");
var alertSystem = new AlertSystem();

shutdownManager.ServerShuttingDown += primaryBackup.CreateBackup;
shutdownManager.ServerShuttingDown += secondaryBackup.CreateBackup;
shutdownManager.ServerShuttingDown += alertSystem.NotifyAdmin;

shutdownManager.InitiateShutdown("Плановое обслуживание");

Console.WriteLine("\n--- Отписка вторичного бэкапа ---\n");

shutdownManager.ServerShuttingDown -= secondaryBackup.CreateBackup;

shutdownManager.InitiateShutdown("Критическая ошибка системы");

return;

public delegate void ServerShutdownEventHandler(object sender, ServerShutdownEventArgs e);