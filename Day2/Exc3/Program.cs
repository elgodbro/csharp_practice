using Exc3;

Console.Write("Введите число A: ");
var a = int.Parse(Console.ReadLine());

Console.Write("Введите число B: ");
var b = int.Parse(Console.ReadLine());

var calculator = new Calculator(a, b);
var result = calculator.CalculateSumOfSquares();

Console.WriteLine($"Сумма квадратов от {a} до {b}: {result}");