using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace SecurityHelpers
{
    public static class StaticSymmetricCryptographyHelpers
    {
        public static string EncryptString(string text, string InitializedValue, string key)
        {
            try
            {
                Aes cipher = CreateCipher(key);
                cipher.IV = Convert.FromBase64String(InitializedValue);

                ICryptoTransform cryptTransform = cipher.CreateEncryptor();
                byte[] plaintext = Encoding.UTF8.GetBytes(text);
                byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

                return Convert.ToBase64String(cipherText);
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }


        public static string DecryptString(string encryptedText, string InitializedValue, string key)
        {
            try
            {
                Aes cipher = CreateCipher(key);
                cipher.IV = Convert.FromBase64String(InitializedValue);

                ICryptoTransform cryptTransform = cipher.CreateDecryptor();
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] plainBytes = cryptTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }

        public static (string Key, string IVBase64) GenerateSymmetricEncryptionKey()
        {
            string key = GetEncodedRandomString(32); // 256
            Aes cipher = CreateCipher(key);
            string IVBase64 = Convert.ToBase64String(cipher.IV);
            return (key, IVBase64);
        }

        private static string GetEncodedRandomString(int length)
        {
            string base64 = Convert.ToBase64String(GenerateRandomBytes(length));
            return base64;
        }

        private static Aes CreateCipher(string keyBase64)
        {
            // Default values: Keysize 256, Padding PKC27
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC

            cipher.Padding = PaddingMode.ISO10126;
            cipher.Key = Convert.FromBase64String(keyBase64);

            return cipher;
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            byte[] byteArray = new byte[length];
            RandomNumberGenerator.Create().GetBytes(byteArray);
            return byteArray;
        }
    }
}
