namespace Exc9;

public class FunctionTabulator
{
    private readonly double _a, _b, _h;
    private readonly int _m;
    
    public FunctionTabulator(double a, double b, int m)
    {
        if (a >= b)
            throw new ArgumentException("A должно быть меньше B");
        if (m <= 0)
            throw new ArgumentException("M должно быть положительным числом");

        _a = a;
        _b = b;
        _m = m;
        _h = (_b - _a) / _m;
    }
    
    public void Tabulate()
    {
        Console.WriteLine("\nТаблица значений функции F(x) = arctg(x):");
        Console.WriteLine("x\tF(x)");

        for (var x = _a; x <= _b; x += _h)
        {
            var fx = Math.Atan(x);
            Console.WriteLine($"{x:F3}\t{fx:F6}");
        }
    }
}