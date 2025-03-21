using Exc1;

var a = new A(10, 5);
Console.WriteLine($"Праметр a: {a.a}" +
                  $"\nПраметр b: {a.b}" +
                  $"\nМетод вычисления значения выражения: {a.Calculate():F2}" +
                  $"\nМетод возведения в квадрат частного a и b: {a.CalculatePow():F2}");
