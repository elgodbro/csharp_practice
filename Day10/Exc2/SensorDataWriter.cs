namespace Exc2;

public class SensorDataWriter(string filePath = "file.data")
{
    public void ClearAndWrite(List<SensorData> sensorData)
    {
        using var writer = new StreamWriter(filePath);
        foreach (var sd in sensorData)
        {
            writer.WriteLine($"{sd.Timestamp};{sd.Value:F2}");
        }
    }
}