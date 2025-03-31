namespace Exc4;

public class FileWatcher
{
    private readonly FileSystemWatcher _watcher;
    private readonly string _archivePath;

    public FileWatcher(string directoryPath)
    {
        _archivePath = Path.Combine(directoryPath, "archive");
        
        if (!Directory.Exists(_archivePath))
            Directory.CreateDirectory(_archivePath);
        
        _watcher = new FileSystemWatcher(directoryPath)
        {
            IncludeSubdirectories = false,
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime
        };
        
        _watcher.Created += OnCreated;
        _watcher.Deleted += OnDeleted;
        _watcher.Changed += OnChanged;
        _watcher.Renamed += OnRenamed;
    }
    
    public void Start()
    {
        _watcher.EnableRaisingEvents = true;
        Console.WriteLine("FileWatcher запущен...");
    }
    
    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
        Console.WriteLine("FileWatcher остановлен");
    }
    
    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[Created] Файл создан: {e.FullPath}");
    }
    
    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[Deleted] Файл удалён: {e.FullPath}");
    }
    
    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"[Changed] Файл изменён: {e.FullPath}");
        MoveOldFilesToArchive(e.FullPath);
    }
    
    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"[Renamed] Файл переименован: {e.OldFullPath} -> {e.FullPath}");
    }
    
    public void MoveOldFilesToArchive(string filePath)
    {
        Console.WriteLine("Проврка файлов...");
        var archivedCount = 0;

        try
        {
            if (!File.Exists(filePath))
                return;

            var lastWrite = File.GetLastWriteTime(filePath);
            if (!((DateTime.Now - lastWrite).TotalDays > 30)) return;

            var destFileName = Path.Combine(_archivePath, Path.GetFileName(filePath));
            if (File.Exists(destFileName))
            {
                destFileName = Path.Combine(_archivePath,
                    $"{Path.GetFileNameWithoutExtension(filePath)}_{DateTime.Now.Ticks}{Path.GetExtension(filePath)}");
            }

            File.Move(filePath, destFileName);
            Console.WriteLine($"Файл перемещён в архив: {destFileName}");
            archivedCount++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при перемещении файла в архив: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Архивировано: {archivedCount}");
        }
    }
}