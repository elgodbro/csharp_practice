using Exc5;

Console.Write("Введите двухзначное число: ");
var num = int.Parse(Console.ReadLine());

var checker = new NumberChecker(num);
var result = checker.IsSumEven();

Console.WriteLine(result ? "Сумма цифр четная" : "Сумма цифр нечетная");