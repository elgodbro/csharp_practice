var matrix = new int[5, 5];

for (var i = 0; i < matrix.GetLength(0); i++)
{
    for (var j = 0; j < matrix.GetLength(1); j++)
    {
        matrix[i, j] = Random.Shared.Next(-50, 51);
    }
}
PrintMatrix();

Console.Write($"Введите номер строки: ");
var rowNumber = int.Parse(Console.ReadLine());
if (rowNumber - 1 < 0 || rowNumber - 1 >= matrix.GetLength(0))
    throw new ArgumentException("Некорректный номер строки");

var sum = 0;
for (var j = 0; j < matrix.GetLength(1); j++)
{
    sum += matrix[rowNumber - 1, j];
}
Console.WriteLine($"Сумма элементов строки {rowNumber}: {sum}");

Console.WriteLine(sum % 10 == 0 ? "Сумма оканчивается на 0" : "Сумма не оканчивается на 0");

return;

void PrintMatrix()
{
    for (var i = 0; i < matrix.GetLength(0); i++)
    {
        for (var j = 0; j < matrix.GetLength(1); j++)
        {
            Console.Write($"{matrix[i, j],3} ");
        }
        Console.WriteLine();
    }
}