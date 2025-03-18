Console.Write("Введите трёхзначное число: ");
var number = int.Parse(Console.ReadLine());

if (number is < 100 or > 999)
{
    Console.WriteLine("Ошибка: число должно быть трёхзначным");
    return;
}

var firstDigit = number / 100;
var lastDigit = number % 10;

Console.WriteLine($"Первая цифра: {firstDigit}");
Console.WriteLine($"Последняя цифра: {lastDigit}");