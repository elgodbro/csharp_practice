using Exc2;

var sensorDatas = new List<SensorData>{};

for (var i = 1; i <= 20; i++)
{
    sensorDatas.Add(new SensorData());
}

var writer = new SensorDataWriter();
writer.ClearAndWrite(sensorDatas);

Console.WriteLine("Содержимое файла:");
Console.WriteLine(File.ReadAllText("file.data"));