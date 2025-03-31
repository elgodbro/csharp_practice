using Exc4;

const string folderToWatch = @".";

var watcher = new FileWatcher(folderToWatch);
watcher.Start();

Console.WriteLine("FileWatcher запущен. Нажмите 'a' для немедленной проверки архива или 'q' для выхода");
                    
var exit = false;
while (!exit)
{
    var key = Console.ReadKey(true);
    switch (key.KeyChar)
    {
        case 'a':
            watcher.MoveOldFilesToArchive(folderToWatch);
            break;
        case 'q':
            exit = true;
            break;
    }
}
                    
watcher.Stop();