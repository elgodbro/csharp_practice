using System.Globalization;

Console.Write("Введите размер матрицы N (N < 10): ");
var N = int.Parse(Console.ReadLine());
if (N is >= 10 or <= 0)
    throw new ArgumentException("N должно быть в диапазоне (0 < N < 10)");

Console.Write("Введите a: ");
var a = int.Parse(Console.ReadLine());
Console.Write("Введите b: ");
var b = int.Parse(Console.ReadLine());

if (a > b)
    throw new ArgumentException("a должно быть <= b");

var matrix = new int[N, N];
for (var i = 0; i < N; i++)
{
    for (var j = 0; j < N; j++)
    {
        matrix[i, j] = Random.Shared.Next(a, b + 1);
    }
}

PrintMatrix();

Console.Write("\nВведите E (нижняя граница, не включая): ");
var E = int.Parse(Console.ReadLine());
Console.Write("Введите F (верхняя граница, включая): ");
var F = int.Parse(Console.ReadLine());

if (E >= F)
    throw new ArgumentException("E должно быть < F");
    
var sumSquares = 0;
foreach (var value in matrix)
{
    if (value > E && value <= F)
        sumSquares += value * value;
}
Console.WriteLine($"Сумма квадратов элементов в промежутке ({E}, {F}]: {sumSquares}");

Console.Write($"\nВведите номер столбца k (1 до {N}): ");
var k = int.Parse(Console.ReadLine());

if (k - 1 < 0 || k - 1 >= N)
    throw new ArgumentException("Неверное значение k");

var sumColumn = 0;
for (var i = 0; i < N; i++)
{
    sumColumn += matrix[i, k - 1];
}

Console.WriteLine($"Сумма элементов {k}-го столбца: {sumColumn}");

return;

void PrintMatrix()
{
    for (var i = 0; i < N; i++)
    {
        for (var j = 0; j < N; j++)
        {
            Console.Write($"{matrix[i, j],3} ");
        }
        Console.WriteLine();
    }
}