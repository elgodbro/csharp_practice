Console.Write("Введите трёхзначное число: ");

if (int.TryParse(Console.ReadLine(), out var number))
{
    if (number is < 100 or > 999)
        throw new ArgumentException("Число должно быть трёхзначным");

    var firstDigit = number / 100;
    var secondDigit = (number / 10) % 10;
    var thirdDigit = number % 10;

    var newNumber = firstDigit * 100 + thirdDigit * 10 + secondDigit;

    Console.WriteLine($"Число после перестановки второй и третьей цифр: {newNumber}");
} else Console.WriteLine("Неверный формат числа");