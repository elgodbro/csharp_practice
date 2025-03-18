namespace Exc5;

public class NumberChecker
{
    private readonly int _number;
    
    public NumberChecker(int number)
    {
        if (number is < 10 or > 99)
            throw new ArgumentException("Число должно быть двухзначным");

        this._number = number;
    }
    
    public bool IsSumEven()
    {
        var firstDigit = _number / 10;
        var secondDigit = _number % 10;
        var sum = firstDigit + secondDigit;

        return sum % 2 == 0;
    }
}