using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using System.Text;
namespace Helpers.EncryptionHelper;

public static class EncryptionHelper
{
    public static void DeriveKeyAndIVFromPassword(string passphrase, out byte[] salt, out byte[] key, out byte[] iv)
    {
        salt = RandomNumberGenerator.GetBytes(8);
        int iterationCount = 1;
        PbeParametersGenerator pbeParametersGenerator = new OpenSslPbeParametersGenerator();
        pbeParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(passphrase.ToCharArray()), salt, iterationCount);
        ParametersWithIV parametersWithIV = (ParametersWithIV)pbeParametersGenerator.GenerateDerivedParameters("AES", 256, 128);
        KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
        key = keyParameter.GetKey();
        iv = parametersWithIV.GetIV();
    }

    public static string FormatResult(byte[] salt, byte[] ciphertext)
    {
        byte[] prefix = Encoding.ASCII.GetBytes("Salted__");
        byte[] prefixSaltCiphertext = new byte[prefix.Length + salt.Length + ciphertext.Length];
        Buffer.BlockCopy(prefix, 0, prefixSaltCiphertext, 0, prefix.Length);
        Buffer.BlockCopy(salt, 0, prefixSaltCiphertext, prefix.Length, salt.Length);
        Buffer.BlockCopy(ciphertext, 0, prefixSaltCiphertext, prefix.Length + salt.Length, ciphertext.Length);
        return Convert.ToBase64String(prefixSaltCiphertext);
    }

    private static void ExtractSaltAndData(string base64Data, out byte[] salt, out byte[] encryptedData)
    {
        byte[] data = Convert.FromBase64String(base64Data);
        byte[] prefix = Encoding.ASCII.GetBytes("Salted__");
        salt = new byte[8];
        encryptedData = new byte[data.Length - prefix.Length - salt.Length];
        Buffer.BlockCopy(data, prefix.Length, salt, 0, salt.Length);
        Buffer.BlockCopy(data, prefix.Length + salt.Length, encryptedData, 0, encryptedData.Length);
    }

    public static string EncryptAES(string plainText, string passphrase)
    {
        using (Aes aesAlg = Aes.Create())
        {
            byte[] salt, key, iv;
            DeriveKeyAndIVFromPassword(passphrase, out salt, out key, out iv);
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.Mode = CipherMode.CBC;

            using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
            {
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return FormatResult(salt, msEncrypt.ToArray());
                }
            }
        }
    }

    public static string DecryptAES(string cipherText, string passphrase)
    {
        byte[] salt, encryptedData;
        ExtractSaltAndData(cipherText, out salt, out encryptedData);

        using (Aes aesAlg = Aes.Create())
        {
            byte[] key, iv;
            int iterationCount = 1;
            PbeParametersGenerator pbeParametersGenerator = new OpenSslPbeParametersGenerator();
            pbeParametersGenerator.Init(PbeParametersGenerator.Pkcs5PasswordToBytes(passphrase.ToCharArray()), salt, iterationCount);
            ParametersWithIV parametersWithIV = (ParametersWithIV)pbeParametersGenerator.GenerateDerivedParameters("AES", 256, 128);
            KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
            key = keyParameter.GetKey();
            iv = parametersWithIV.GetIV();

            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.Mode = CipherMode.CBC;

            using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            {
                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

    public static string GenerateKey()
    {
        byte[] key = new byte[32]; // 256 bits
        RandomNumberGenerator.Fill(key);
        return Convert.ToBase64String(key);
    }

   
}
