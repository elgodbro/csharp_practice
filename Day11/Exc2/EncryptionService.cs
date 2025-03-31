namespace Exc2;

public class EncryptionService(IEncryptionStrategy strategy = null!)
{
    private IEncryptionStrategy _strategy = strategy ?? new NoEncryption();

    public void SetStrategy(IEncryptionStrategy strategy)
    {
        _strategy = strategy;
    }

    public string EncryptData(string data) => _strategy.Encrypt(data);

    public string DecryptData(string encryptedData) => _strategy.Decrypt(encryptedData);
}