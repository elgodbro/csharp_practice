using Spectre.Console;

namespace Exc1;

public class Warehouse(int initialStock)
{
    private int _currentStock = initialStock;
    
    public void CheckStock(int requestedQuantity)
    {
        if (requestedQuantity <= _currentStock)
        {
            _currentStock -= requestedQuantity;
            AnsiConsole.Write(new Panel($"[green bold]ОК.[/] Остаток: {_currentStock} шт").BorderColor(Color.Green));
        } else
        {
            throw new OutOfStockException(
                $"Недостаточно шт на складе. Запрошено: {requestedQuantity}, Имеется: {_currentStock}"
            );
        }
    }
}