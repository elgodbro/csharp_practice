namespace Exc3;

public class SensorData(DateTime time, double value)
{
    public DateTime Timestamp { get; set; } = time;
    public double Value { get; set; } =  value;
}