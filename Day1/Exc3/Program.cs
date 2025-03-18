Console.Write("Введите x: ");
if (double.TryParse(Console.ReadLine(), out var x))
{
    var z1Discriminant = Math.Pow(x, 2) - 9;

    if (z1Discriminant < 0)
        throw new ArgumentException("Выражение под корнем отрицательное");


    var sqrtValue = Math.Sqrt(z1Discriminant);

    var z1Top = Math.Pow(x, 2) + 2 * x - 3 + (x + 1) * sqrtValue;
    var z1Bottom = Math.Pow(x, 2) - 2 * x - 3 + (x - 1) * sqrtValue;

    if (z1Bottom == 0)
        throw new DivideByZeroException();
    
    var z1 = z1Top / z1Bottom;
    Console.WriteLine($"z1 = {z1}");


    var z2Top = x + 3;
    var z2Bottom = x - 3;
    
    if (z2Bottom == 0)
        throw new DivideByZeroException();

    var z2Discriminant = z2Top / z2Bottom;
    if (z2Discriminant < 0)
        throw new ArgumentException("Выражение под корнем отрицательное");
    
    var z2 = Math.Sqrt(z2Discriminant);
    Console.WriteLine($"z2 = {z2}");
} else Console.WriteLine("Неверный формат числа");