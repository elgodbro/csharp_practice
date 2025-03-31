using System.Security.Cryptography;
using Exc2;

var service = new EncryptionService();

var aesKey = new byte[32];
var aesIV = new byte[16];
var desKey = new byte[8];
var desIV = new byte[8];

RandomNumberGenerator.Fill(aesKey);
RandomNumberGenerator.Fill(aesIV);
RandomNumberGenerator.Fill(desKey);
RandomNumberGenerator.Fill(desIV);

const string data = "Секретные данные";
Console.WriteLine($"{service.EncryptData(data)}\n");

service.SetStrategy(new AESEncryption(aesKey, aesIV));
var encrypted = service.EncryptData(data);
Console.WriteLine($"AES: {encrypted}");
Console.WriteLine($"{service.DecryptData(encrypted)}\n");

service.SetStrategy(new DESEncryption(desKey, desIV));
encrypted = service.EncryptData(data);
Console.WriteLine($"DES: {encrypted}");
Console.WriteLine(service.DecryptData(encrypted));