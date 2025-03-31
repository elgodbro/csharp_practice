namespace Exc2;

public class NoEncryption : IEncryptionStrategy
{
    public string Encrypt(string data)
    {
        Console.WriteLine("Без шифрования");
        return data;
    }

    public string Decrypt(string encryptedData)
    {
        Console.WriteLine("Нет необходимости в дешифровании");
        return encryptedData;
    }
}