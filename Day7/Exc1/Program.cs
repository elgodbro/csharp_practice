using Exc1;

Console.Write("Введите число: ");
var number = int.Parse(Console.ReadLine());

var binaryConverter = new BinaryConverter();
var hexConverter = new HexConverter();

NumberConverter toBinary = binaryConverter.Convert;
NumberConverter toHex = hexConverter.Convert;

Console.WriteLine($"В двоичной системе: {toBinary(number)}");
Console.WriteLine($"В шестнадцатеричной системе: {toHex(number)}");

var multiConverter = toBinary;
multiConverter += toHex;
        
Console.WriteLine($"Результат мультикаст делегата: {multiConverter(number)}");

return;

delegate string NumberConverter(int number);