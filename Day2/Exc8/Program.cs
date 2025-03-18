using Exc8;

Console.Write("Введите A: ");
var a = int.Parse(Console.ReadLine());

Console.Write("Введите B: ");
var b = int.Parse(Console.ReadLine());

var sequence = new NumberSequence(a, b);
sequence.PrintDescending();