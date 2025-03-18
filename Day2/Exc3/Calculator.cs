namespace Exc3;

public class Calculator
{
    private readonly int _a,  _b;
    
    public Calculator(int a, int b)
    {
        if (a < 1 || b > 10 || a >= b)
            throw new ArgumentException("Числа должны удовлетворять условию: 1 <= A < B <= 10");

        this._a = a;
        this._b = b;
    }
    
    public int CalculateSumOfSquares()
    {
        var sum = 0;
        for (var i = _a; i <= _b; i++)
        {
            sum += i * i;
        }
        return sum;
    }
}