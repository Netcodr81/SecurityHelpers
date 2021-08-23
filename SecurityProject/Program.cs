using SecurityHelpers;
using SecurityHelpers.BaseClasses;
using SecurityHelpers.Helpers;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

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

            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                  Asymmetric Encryption                                         ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            //RsaEncryption asyHelper = new RsaEncryption();
            //string publicKey = asyHelper.GetPublicKey();
            //string privatekey = asyHelper.GetPublicKey();
            //string asymmetricOriginalText = "This is the original text";
            //string asymmetricEncryptedText = asyHelper.Encrypt(asymmetricOriginalText, publicKey);
            //string asymmetricDecryptedText = asyHelper.Decrypt(asymmetricEncryptedText, privatekey);
            NewRSA asymmetricHelper = new NewRSA();
            string publicKey = asymmetricHelper.GetPrivateKey();
            string privateKey = asymmetricHelper.GetPrivateKey();
            string asymmetricOriginalText = "Original asymmetric text";
            string asymmetricEncryptedText = asymmetricHelper.Encrypt(asymmetricOriginalText, publicKey);
            string asymmetricDecryptedText = asymmetricHelper.Decrypt(asymmetricEncryptedText, privateKey);

            Console.WriteLine($"Public key: {publicKey}");
            Console.WriteLine($"Private key: {privateKey}");
            Console.WriteLine($"Original text: {asymmetricOriginalText}");
            Console.WriteLine($"Encrypted text: {asymmetricEncryptedText}");
            Console.WriteLine($"Decrypted text: {asymmetricDecryptedText}");

            Console.ReadLine();
        }
    }
}
