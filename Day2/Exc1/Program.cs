using Exc1;

Console.Write("Введите радиус окружности: ");
var radius = double.Parse(Console.ReadLine());

var circle = new Circle(radius);
var diameter = circle.GetDiameter();

Console.WriteLine($"Диаметр окружности: {diameter}");