using Exc2;
using Spectre.Console;

var handler = new CollectionHandler();
var numbers = new List<int> { 1, 2, 3, 4, 5 };

try
{
    var result = handler.ProcessList(numbers, 12);
    AnsiConsole.Write(new Panel($"Полученное значение: [green bold]{result}[/]").BorderColor(Color.Green).Header("Результат вызова"));
}
catch (CollectionException ex)
{
    CollectionHandler.LogException(ex);
}