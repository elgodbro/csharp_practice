Console.Write("Введите первое число: ");
var a = int.Parse(Console.ReadLine());

Console.Write("Введите второе число: ");
var b = int.Parse(Console.ReadLine());

Console.Write("Введите третье число: ");
var c = int.Parse(Console.ReadLine());

Console.WriteLine($"Произведение чисел ({a} * {b} * {c}) = {a * b * c}");
Console.WriteLine($"Произведение чисел в обратном порядке ({c} * {b} * {a}) = {c * b * a}");