namespace Exc2;

public interface IEncryptionStrategy
{
    string Encrypt(string data);
    string Decrypt(string encryptedData);
}