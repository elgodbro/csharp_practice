Console.Write("Введите число: ");
var number = int.Parse(Console.ReadLine());
Console.WriteLine(IsPrime(number) ? "Число является простым" : "Число не является простым");


return;

static bool IsPrime(int num)
{
    if (num < 2) return false;
    if (num == 2) return true;
    if (num % 2 == 0) return false;

    var boundary = (int)Math.Sqrt(num);
    for (var i = 3; i <= boundary; i += 2)
    {
        if (num % i == 0)
            return false;
    }
    return true;
}