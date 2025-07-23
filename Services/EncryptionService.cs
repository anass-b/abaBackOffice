using abaBackOffice.Interfaces.Services;
using Helpers.EncryptionHelper;

namespace abaBackOffice.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly ILogger<EncryptionService> _logger;

        public EncryptionService(ILogger<EncryptionService> logger)
        {
            _logger = logger;
        }

        public string Encrypt(string plainText, string passphrase)
        {
            try
            {
                if (string.IsNullOrEmpty(plainText))
                {
                    _logger.LogInformation("Plain text is null or empty, returning input as is.");
                    return plainText;
                }

                _logger.LogInformation("Encrypting text");
                return EncryptionHelper.EncryptAES(plainText, passphrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encrypting text");
                throw;
            }
        }

        public string Decrypt(string cipherText, string passphrase)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                {
                    _logger.LogInformation("Cipher text is null or empty, returning input as is.");
                    return cipherText;
                }

                _logger.LogInformation("Decrypting text");
                return EncryptionHelper.DecryptAES(cipherText, passphrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error decrypting text");
                throw;
            }
        }

        public bool CompareDecryptedWithEncrypted(string decryptedValue, string encryptedValue, string passphrase)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptedValue) || string.IsNullOrEmpty(encryptedValue))
                {
                    _logger.LogInformation("One or both values are null or empty. Returning false.");
                    return false;
                }

                _logger.LogInformation("Decrypting and comparing values for similarity.");

                // Decrypt the encrypted value
                string decryptedEncryptedValue = EncryptionHelper.DecryptAES(encryptedValue, passphrase);

                // Compare the decrypted value with the decrypted encrypted value
                return string.Equals(decryptedValue, decryptedEncryptedValue, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comparing decrypted and encrypted values.");
                return false; // Return false if there's any error during decryption or comparison
            }
        }

    }
}