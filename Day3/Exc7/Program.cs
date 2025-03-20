using System.Text;

Console.Write("Введите исходную строку: ");
var original = Console.ReadLine();

Console.Write("Введите подстроку для вставки: ");
var substring = Console.ReadLine();

Console.Write("Введите позицию для вставки: ");
var position = int.Parse(Console.ReadLine());

if (position < 0) position = 0;
if (position > original.Length) position = original.Length;

var sb = new StringBuilder(original);
sb.Insert(position, substring);

Console.WriteLine($"Результат: {sb}");