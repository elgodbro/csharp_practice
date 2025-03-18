using Exc6;

Console.Write("Введите номер месяца (1-12): ");
var month = int.Parse(Console.ReadLine());

var calculator = new MonthCalculator(month);
var monthsLeft = calculator.MonthsLeft();

Console.WriteLine($"До конца года осталось {monthsLeft} месяцев.");