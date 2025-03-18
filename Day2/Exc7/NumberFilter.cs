namespace Exc7;

public class NumberFilter
{
    private readonly int _a, _b, _x;
    
    public NumberFilter(int a, int b, int x)
    {
        if (a > b)
            throw new ArgumentException("Числа должны удовлетворять условию: A ≤ B");
        if (x is < 0 or > 9)
            throw new ArgumentException("X должен быть одной цифрой (0-9)");

        _a = a;
        _b = b;
        _x = x;
    }
    
    public void PrintNumbersWhile()
    {
        Console.WriteLine("\nМетод: while");
        var i = _a;
        while (i <= _b)
        {
            if (i % 10 == _x)
                Console.Write(i + " ");
            i++;
        }
        Console.WriteLine();
    }
    
    public void PrintNumbersDoWhile()
    {
        Console.WriteLine("\nМетод: do-while");
        var i = _a;
        do
        {
            if (i % 10 == _x)
                Console.Write(i + " ");
            i++;
        } while (i <= _b);
        Console.WriteLine();
    }
    
    public void PrintNumbersFor()
    {
        Console.WriteLine("\nМетод: for");
        for (var i = _a; i <= _b; i++)
        {
            if (i % 10 == _x)
                Console.Write(i + " ");
        }
        Console.WriteLine();
    }
}