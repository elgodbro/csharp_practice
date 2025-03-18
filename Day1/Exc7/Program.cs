Console.Write("Введите сторону a: ");
var a = double.Parse(Console.ReadLine());

Console.Write("Введите сторону b: ");
var b = double.Parse(Console.ReadLine());

Console.Write("Введите сторону c: ");
var c = double.Parse(Console.ReadLine());

var p = (a + b + c) / 2;
var area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

Console.WriteLine($"Площадь треугольника: {area}");