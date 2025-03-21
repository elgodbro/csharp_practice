namespace Exc1;

public class A(int _a, int _b)
{
    public int a { get; set; } = _a;
    public int b { get; set; } = _b;

    public double Calculate()
    {
        return Math.Pow(b, 3) -4 * Math.Sqrt(a);
    }

    public double CalculatePow()
    {
        return Math.Pow(a / b, 2);
    }
}