using System.Security.Cryptography;

namespace Exc2;

public class DESEncryption(byte[] key, byte[] iv) : IEncryptionStrategy
{
    public string Encrypt(string data)
    {
        using var desAlg = DES.Create();
        desAlg.Key = key;
        desAlg.IV = iv;

        var encryptor = desAlg.CreateEncryptor(desAlg.Key, desAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(data);
        }
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public string Decrypt(string encryptedData)
    {
        var buffer = Convert.FromBase64String(encryptedData);

        using var desAlg = DES.Create();
        desAlg.Key = key;
        desAlg.IV = iv;

        var decryptor = desAlg.CreateDecryptor(desAlg.Key, desAlg.IV);

        using var msDecrypt = new MemoryStream(buffer);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}