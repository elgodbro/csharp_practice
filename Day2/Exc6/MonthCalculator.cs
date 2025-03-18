namespace Exc6;

public class MonthCalculator
{
    private readonly int _month;
    
    public MonthCalculator(int month)
    {
        if (month is < 1 or > 12)
            throw new ArgumentException("Номер месяца должен быть от 1 до 12");

        this._month = month;
    }
    
    public int MonthsLeft()
    {
        return _month switch
        {
            1 => 11,
            2 => 10,
            3 => 9,
            4 => 8,
            5 => 7,
            6 => 6,
            7 => 5,
            8 => 4,
            9 => 3,
            10 => 2,
            11 => 1,
            12 => 0,
            _ => -1
        };
    }
}