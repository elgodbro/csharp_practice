namespace Exc4;

public static class DoubleExtension
{
    public static double RoundToDigits(this double number, int digits)
    {
        if (digits < 0)
            throw new ArgumentException("Количество знаков не может быть отрицательным");
            
        return Math.Round(number, digits);
    }
}