var x = 2.7;

var yPart1 = 3 * Math.Pow(x, 2);
var yPart2 = Math.Exp(Math.Sqrt(x)) / 2 * Math.PI;
var yPart3 = Math.Log10(Math.Sqrt(Math.Abs(3 - Math.Pow(Math.Sin(x), 2))));

var y = yPart1 + yPart2 - yPart3;
Console.WriteLine($"y = {y}");