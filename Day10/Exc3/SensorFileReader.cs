namespace Exc3;

public class SensorFileReader
{
    private const string FileName = "file.data";
    
    public List<SensorData> ReadSensorData()
    {
        var sensorDataList = new List<SensorData>();

        if (!File.Exists(FileName))
        {
            Console.WriteLine("Файл не найден!");
            return sensorDataList;
        }

        foreach (var line in File.ReadLines(FileName))
        {
            var parts = line.Split(';');
            if (parts.Length == 2 && DateTime.TryParse(parts[0], out DateTime timestamp) && double.TryParse(parts[1], out double value))
                sensorDataList.Add(new SensorData(timestamp, value));
        }

        return sensorDataList;
    }
}