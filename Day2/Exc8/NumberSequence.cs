namespace Exc8;

public class NumberSequence
{
    private readonly int _a, _b;
    
    public NumberSequence(int a, int b)
    {
        if (a < 1 || b > 100 || a >= b)
            throw new ArgumentException("Числа должны удовлетворять условию: 1 ≤ A < B ≤ 100");

        _a = a;
        _b = b;
    }
    
    public void PrintDescending()
    {
        Console.WriteLine("\nЧисла в порядке убывания:");

        var count = 0;
        for (var i = _b - 1; i > _a; i--)
        {
            Console.Write(i + " ");
            count++;
        }

        Console.WriteLine($"\nКоличество чисел: {count}");
    }
}