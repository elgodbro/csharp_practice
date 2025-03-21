Console.Write("Введите основание: ");
var a = int.Parse(Console.ReadLine());

Console.Write("Введите показатель степени: ");
var b = int.Parse(Console.ReadLine());

Console.WriteLine($"Результат: {Power(a, b):F2}");

return;

static double Power(double baseNum, int exponent)
{
    return exponent switch
    {
        0 => 1,
        < 0 => 1 / Power(baseNum, -exponent),
        _ => baseNum * Power(baseNum, exponent - 1)
    };
}