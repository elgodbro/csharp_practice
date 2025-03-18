using Exc2;

Console.Write("Введите четырёхзначное число: ");
var num = int.Parse(Console.ReadLine());

var checker = new NumberChecker(num);
var result = checker.IsSumEqual();

Console.WriteLine(result ? "Суммы равны" : "Суммы не равны");