using Exc1;

var logger = new Logger();

logger.AddLog("Запуск приложения");
logger.AddLog("Выполнена первая операция");
logger.AddLog("Выполнена вторая операция");

Console.WriteLine("Логи:");
logger.PrintLogs();

Console.WriteLine("\nЛоги, содержащие 'первая':");
var foundLogs = logger.Search("первая");
foreach (var log in foundLogs)
{
    Console.WriteLine(log);
}

Console.WriteLine("\nСортировка по дате:");
var sortedLogs = logger.GetSortedLogs();
foreach (var log in sortedLogs)
{
    Console.WriteLine(log);
}

Console.WriteLine("\nЛоги за сегодня:");
var todayLogs = logger.FilterByDate(DateTime.Now);
foreach (var log in todayLogs)
{
    Console.WriteLine(log);
}

var removedLog = logger.RemoveLog();
Console.WriteLine("\nУдалено: " + removedLog);

Console.WriteLine("\nЛоги:");
logger.PrintLogs();