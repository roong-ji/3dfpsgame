using System;
using System.Security.Cryptography;
using System.Text;

public static class AES
{
    private const int IVSize = 16;

    public static string Encrypt(string text, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Hash.GetHashKey(key);
            aes.GenerateIV();
            byte[] iv = aes.IV;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            byte[] encryptedBytes = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);

            byte[] result = new byte[iv.Length + encryptedBytes.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }
    }

    public static string Decrypt(string encryptedText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Hash.GetHashKey(key);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            byte[] iv = new byte[IVSize];
            Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);
            aes.IV = iv;

            int ciphertextLength = encryptedBytes.Length - iv.Length;
            byte[] ciphertext = new byte[ciphertextLength];
            Buffer.BlockCopy(encryptedBytes, iv.Length, ciphertext, 0, ciphertextLength);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
