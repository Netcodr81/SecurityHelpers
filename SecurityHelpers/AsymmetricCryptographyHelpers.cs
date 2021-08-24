using SecurityHelpers.Contracts;
using SecurityHelpers.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecurityHelpers
{
    public class AsymmetricCryptographyHelpers : IAsymmetricCryptographyHelpers
    {
        public AsymmetricCryptographyHelpers()
        {

        }

        public (string publicKey, string privateKey) GenerateKeys(int keyLength = 2048)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = keyLength;
                return (publicKey: rsa.ExportParameters(includePrivateParameters: false).RSAParameterToString(), privateKey: rsa.ExportParameters(includePrivateParameters: true).RSAParameterToString());
            }
        }

        public string Encrypt(string data, string publicKey)
        {
            try
            {
                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportParameters(publicKey.ToRSAParameter());

                    byte[] result = rsa.Encrypt(Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.OaepSHA256);
                    return Convert.ToBase64String(result);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string Decrypt(string data, string privateKey)
        {
            try
            {
                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportParameters(privateKey.ToRSAParameter());
                    return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(data), RSAEncryptionPadding.OaepSHA256));
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        public void GenerateAndExportKeysToFile(string filePath, string filename = "keys")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("File path can't be blank");
            }

            (string publicKey, string privateKey) keys = this.GenerateKeys();

            try
            {
                string fileName = Path.Combine(filePath, $"{filename}.txt");
                FileInfo fileInfo = new FileInfo(fileName);

                using (StreamWriter streamWriter = fileInfo.CreateText())
                {
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"             Keys - Generated on {DateTime.Now.ToShortDateString()}            ");
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"Privte Key: {keys.privateKey}");
                    streamWriter.WriteLine($"Public Key: {keys.publicKey}");
                }

            }
            catch (Exception)
            {

                throw new Exception("An error occurred while crating the file");
            }

        }

        public void ExportKeysToFile((string, string) keys, string filePath, string filename = "keys")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("File path can't be blank");
            }

            (string publicKey, string privateKey) keysToExport = keys;

            try
            {
                string fileName = Path.Combine(filePath, $"{filename}.txt");
                FileInfo fileInfo = new FileInfo(fileName);

                using (StreamWriter streamWriter = fileInfo.CreateText())
                {
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"             Keys - Generated on {DateTime.Now.ToShortDateString()}            ");
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"Privte Key: {keysToExport.privateKey}");
                    streamWriter.WriteLine(string.Empty);
                    streamWriter.WriteLine(string.Empty);
                    streamWriter.WriteLine($"Public Key: {keysToExport.publicKey}");

                }

            }
            catch (Exception)
            {

                throw new Exception("An error occurred while crating the file");
            }

        }

    }
}
