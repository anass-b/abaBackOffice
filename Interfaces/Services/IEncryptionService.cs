namespace abaBackOffice.Interfaces.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText, string passphrase);
        string Decrypt(string cipherText, string passphrase);
        bool CompareDecryptedWithEncrypted(string decryptedValue, string encryptedValue, string passphrase);
    }
}