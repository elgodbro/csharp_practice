using System.Diagnostics;
using Spectre.Console;

namespace Exc2;

public class CollectionHandler
{
    private readonly ListProcessor _processor = new ListProcessor();

    public int ProcessList(List<int> list, int index)
    {
        try
        {
            return _processor.GetElementAt(list, index);
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new CollectionException("Ошибка обработки коллекции", ex);
        }
    }

    public static void LogException(Exception ex)
    {
        Console.WriteLine("");    
        var table = new Table().HideHeaders().AddColumn("").AddColumn("").Border(TableBorder.None);;
    
        table.AddRow("[yellow]Тип исключения[/]", $"[red bold]{ex.GetType().Name}[/]");
        table.AddRow("[yellow]Сообщение[/]", ex.Message);
        table.AddRow("[yellow]Стек вызовов[/]", $"{ex.StackTrace.EscapeMarkup()}\n");
    
        if(ex.InnerException != null)
        {
            table.AddRow("[grey]Внут. тип[/]", $"[red bold]{ex.InnerException.GetType().Name}[/]");
            table.AddRow("[grey]Сообщение[/]", ex.InnerException.Message);
            table.AddRow("[grey]Стек вызовов[/]", $"{ex.InnerException.StackTrace.EscapeMarkup()}");
        }
    
        AnsiConsole.Write(new Panel(table).BorderColor(Color.Red).Header("Ошибка при запросе"));
    }
}