using Exc1;
using Spectre.Console;

try
{
    var warehouse = new Warehouse(10);

    Console.WriteLine("Попытка взять 5 предметов:");
    warehouse.CheckStock(5);

    Console.WriteLine("\nПопытка взять 2 предмета:");
    warehouse.CheckStock(2);
    
    Console.WriteLine("\nПопытка взять 10 предметов:");
    warehouse.CheckStock(10);
}
catch (OutOfStockException ex)
{
    AnsiConsole.Write(new Panel($"[red bold]Ошибка:[/] {ex.Message}").BorderColor(Color.Red));
}