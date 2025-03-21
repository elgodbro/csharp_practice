Console.Write("Введите число K: ");
var K = int.Parse(Console.ReadLine());

Console.Write("Введите D1 (1-9): ");
var D1 = int.Parse(Console.ReadLine());
AddLeftDigit(D1, ref K);
Console.WriteLine($"После добавления D1: {K}");

Console.Write("Введите D2 (1-9): ");
var D2 = int.Parse(Console.ReadLine());
AddLeftDigit(D2, ref K);
Console.WriteLine($"После добавления D2: {K}");
return;

static void AddLeftDigit(int D, ref int K)
{
    if (D is < 1 or > 9)
        throw new ArgumentException("D должно быть от 1 до 9");
        
    var digits = 0;
    var temp = K;
    while (temp > 0)
    {
        temp /= 10;
        digits++;
    }
    K = D * (int)Math.Pow(10, digits) + K;
}