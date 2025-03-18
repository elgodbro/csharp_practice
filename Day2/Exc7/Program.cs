using Exc7;

Console.Write("Введите A: ");
var a = int.Parse(Console.ReadLine());

Console.Write("Введите B: ");
var b = int.Parse(Console.ReadLine());

Console.Write("Введите X (0-9): ");
var x = int.Parse(Console.ReadLine());

var filter = new NumberFilter(a, b, x);
filter.PrintNumbersWhile();
filter.PrintNumbersDoWhile();
filter.PrintNumbersFor();