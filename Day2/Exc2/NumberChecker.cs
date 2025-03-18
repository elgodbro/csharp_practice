namespace Exc2;

public class NumberChecker
{
    private readonly int _number;
    
    public NumberChecker(int number)
    {
        if (number is < 1000 or > 9999)
            throw new ArgumentException("Число должно быть четырёхзначным");
        
        this._number = number;
    }
    
    public bool IsSumEqual()
    {
        var firstTwoSum = (_number / 1000) + ((_number / 100) % 10);
        var lastTwoSum = ((_number / 10) % 10) + (_number % 10);

        return firstTwoSum == lastTwoSum;
    }
}