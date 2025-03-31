using Exc3;

var reader = new SensorFileReader();
var processor = new SensorProcessor();

var data = reader.ReadSensorData();
var average = processor.CalculateAverageValue(data);

Console.WriteLine($"Среднее значение сенсоров: {average:F2}");