var rowCount = Random.Shared.Next(3, 10);
var jaggedArray = new int[rowCount][];

for (var i = 0; i < rowCount; i++)
{
    var colCount = Random.Shared.Next(3, 6);
    jaggedArray[i] = new int[colCount];

    for (int j = 0; j < colCount; j++)
    {
        jaggedArray[i][j] = Random.Shared.Next(0, 6);
    }
}

for (var i = 0; i < jaggedArray.Length; i++)
{
    Console.WriteLine($"[{i}]: " + string.Join(", ", jaggedArray[i]));
}

var maxUniqueCount = 0;
var maxUniqueRowIndex = -1;

for (var i = 0; i < jaggedArray.Length; i++)
{
    var uniqueCount = jaggedArray[i].Distinct().Count();
    
    if (uniqueCount > maxUniqueCount)
    {
        maxUniqueCount = uniqueCount;
        maxUniqueRowIndex = i;
    }
}

Console.WriteLine($"\nСтрока с максимальным числом уникальных элементов: {maxUniqueRowIndex} (уникальных: {maxUniqueCount})");