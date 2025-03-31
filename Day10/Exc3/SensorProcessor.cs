namespace Exc3;

public class SensorProcessor
{
    public double CalculateAverageValue(List<SensorData>? sensorData)
    {
        if (sensorData != null && sensorData.Count != 0)
            return sensorData.Average(sd => sd.Value);
        
        Console.WriteLine("Нет данных для анализа.");
        return 0.0;
    }
}