using SecurityHelpers;
using System;

namespace SecurityProject
{
    class Program
    {
        static void Main(string[] args)
        {

            (string Key, string IVBase64) generateKey = StaticSymmetricCryptographyHelpers.GenerateSymmetricEncryptionKey();

            string unencryptedString = "test";
            string encryptedString = StaticSymmetricCryptographyHelpers.EncryptString(unencryptedString, generateKey.IVBase64, generateKey.Key);

            string decryptedString = StaticSymmetricCryptographyHelpers.DecryptString(encryptedString, generateKey.IVBase64, generateKey.Key);

            Console.WriteLine($"Key: {generateKey.Key}");
            Console.WriteLine($"Initialized Value: {generateKey.IVBase64}");
            Console.WriteLine($"Encrypted Text: { encryptedString}");
            Console.WriteLine($"Decrypted Text: { decryptedString}");



            Console.ReadLine();
        }
    }
}
