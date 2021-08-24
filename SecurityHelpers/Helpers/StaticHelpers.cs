using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecurityHelpers.Helpers
{
    public static class StaticHelpers
    {
        public static byte[] GenerateRandomNumber(int length)
        {
            using (RNGCryptoServiceProvider randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        public static Aes CreateCipher(string key)
        {
            // Default values: Keysize 256, Padding PKC27
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC

            cipher.Padding = PaddingMode.PKCS7;
            cipher.Key = Convert.FromBase64String(key);

            return cipher;
        }

        public static string GetEncodedRandomNumberString(int length)
        {
            string base64 = Convert.ToBase64String(GenerateRandomNumber(length));
            return base64;
        }

        public static string GenerateSymmetricKey(int? length = 32)
        {
            return GetEncodedRandomNumberString(length.Value);
        }
        public static string GenerateSymmetricInitialVector(int? length = 16)
        {
            return GetEncodedRandomNumberString(length.Value);
        }


    }
}
