namespace Exc2;

public class SensorData
{
    public DateTime Timestamp { get; set; } = Faker.Date.Past();
    public double Value { get; set; } =  Random.Shared.NextDouble() * (50) - 25;
}