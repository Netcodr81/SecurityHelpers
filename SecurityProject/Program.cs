using SecurityHelpers;
using SecurityHelpers.Helpers;
using System;

namespace SecurityProject
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                  Symmetric Encryption                                          ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            string key = StaticHelpers.GenerateSymmetricKey();
            string iv = StaticHelpers.GenerateSymmetricInitialVector();
            string wrongKey = StaticHelpers.GenerateSymmetricKey();
            SymmetricCryptographyHelper symmetricHelper = new SymmetricCryptographyHelper();

            string originalText = "This is the original text";
            string encryptedText = symmetricHelper.EncryptString(originalText, iv, key);
            string decryptedString = symmetricHelper.DecryptString(encryptedText, iv, key);
            string decryptedWithWrongKey = symmetricHelper.DecryptString(encryptedText, iv, wrongKey);

            Console.WriteLine($"Key: {key}");
            Console.WriteLine($"Initialized Vector: {iv}");
            Console.WriteLine($"Wrong key value: {wrongKey}");
            Console.WriteLine($"Original Text: {originalText}");
            Console.WriteLine($"Encrypted Text: {encryptedText}");
            Console.WriteLine($"Decrypted Text: { decryptedString}");
            Console.WriteLine($"Decrypted with wrong key text: {decryptedWithWrongKey}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                  Asymmetric Encryption                                         ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            AsymmetricCryptographyHelpers asymmetricHelper = new AsymmetricCryptographyHelpers();
            (string publicKey, string privateKey) encryptionKeys = asymmetricHelper.GenerateKeys();
            string publicKey = encryptionKeys.publicKey;
            string privateKey = encryptionKeys.privateKey;
            string asymmetricOriginalText = "Original asymmetric text";
            string asymmetricEncryptedText = asymmetricHelper.Encrypt(asymmetricOriginalText, publicKey);
            string asymmetricDecryptedText = asymmetricHelper.Decrypt(asymmetricEncryptedText, privateKey);


            Console.WriteLine($"Public key: {publicKey}");
            Console.WriteLine();
            Console.WriteLine($"Private key: {privateKey}");
            Console.WriteLine();
            Console.WriteLine($"Original text: {asymmetricOriginalText}");
            Console.WriteLine();
            Console.WriteLine($"Encrypted text: {asymmetricEncryptedText}");
            Console.WriteLine();
            Console.WriteLine($"Decrypted text: {asymmetricDecryptedText}");
            Console.WriteLine();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("Exporting keys to your desktop...");

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            try
            {
                asymmetricHelper.ExportKeysToFile(encryptionKeys, path);

                Console.WriteLine("Keys exported to your desktop");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                          Hashing                                               ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            HashingHelpers hashHelper = new HashingHelpers();
            string textToHash = "This is a sentence to hash";
            string hashKey = "password";


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Text to hash: {textToHash}");
            Console.WriteLine($"Password for HMAC: {hashKey}");
            Console.WriteLine();
            Console.WriteLine($"Non authenticated hash: {hashHelper.ComputeHash(textToHash)}");
            Console.WriteLine();
            Console.WriteLine($"Authenticated hash (HMAC): {hashHelper.ComputeHmac256(hashKey, textToHash)}");

            Console.ReadLine();
        }
    }
}
