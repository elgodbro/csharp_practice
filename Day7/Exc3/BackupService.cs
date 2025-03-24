namespace Exc3;

public class BackupService(string serviceName)
{
    public string ServiceName { get; private set; } = serviceName;
    
    public void CreateBackup(object sender, ServerShutdownEventArgs e)
    {
        Console.WriteLine($"[{ServiceName}] Создан бэкап");
    }
}