namespace Exc10;

public static class MaxValueFinder
{
    public static double FindMaxValue(double[] values)
    {
        var max = values[0];
        foreach (var value in values)
        {
            if (value > max) max = value;
        }
        return max;
    }
}