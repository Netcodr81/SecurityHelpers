using SecurityHelpers.Contracts;
using SecurityHelpers.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SecurityHelpers
{
    public class SymmetricCryptographyHelper : ISymmetricCryptographyHelper
    {
        public SymmetricCryptographyHelper()
        {

        }

        public string EncryptString(string dataToEncrypt, string iv, string key)
        {
            if ((dataToEncrypt == null) || (dataToEncrypt.Length <= 0))
                throw new ArgumentNullException(nameof(dataToEncrypt));
            if ((key == null) || (key.Length <= 0))
                throw new ArgumentNullException(nameof(key));
            if ((iv == null) || (iv.Length <= 0))
                throw new ArgumentNullException(nameof(iv));

            try
            {

                using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    aes.Key = Convert.FromBase64String(key);
                    aes.IV = Convert.FromBase64String(iv);

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream stream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamwriter = new StreamWriter(stream))
                            {

                                //Write all data to the stream.
                                streamwriter.Write(dataToEncrypt);
                            }
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }


        public string DecryptString(string dataToDecrypt, string iv, string key)
        {
            if (string.IsNullOrEmpty(dataToDecrypt))
                throw new ArgumentNullException(nameof(dataToDecrypt));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(iv))
                throw new ArgumentNullException(nameof(iv));


            try
            {
                using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                {

                    aes.Key = Convert.FromBase64String(key);
                    aes.IV = Convert.FromBase64String(iv);


                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(dataToDecrypt)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                return string.Empty;
            }

        }

        public string GenerateInitialVector()
        {
            return StaticHelpers.GenerateSymmetricInitialVector();
        }

        public string GenerateSymmetricKey()
        {
            return StaticHelpers.GenerateSymmetricKey();
        }

        public void GenerateAndExportKeysToFile(string filePath, string filename = "key")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("File path can't be blank");
            }

            string keys = this.GenerateSymmetricKey();

            try
            {
                string fileName = Path.Combine(filePath, $"{filename}.txt");
                FileInfo fileInfo = new FileInfo(fileName);

                using (StreamWriter streamWriter = fileInfo.CreateText())
                {
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"             Key - Generated on {DateTime.Now.ToShortDateString()}            ");
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"Privte Key: {keys}");
                }

            }
            catch (Exception)
            {

                throw new Exception("An error occurred while crating the file");
            }

        }

        public void ExportKeyToFile(string key, string filePath, string filename = "key")
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("File path can't be blank");
            }

            try
            {
                string fileName = Path.Combine(filePath, $"{filename}.txt");
                FileInfo fileInfo = new FileInfo(fileName);

                using (StreamWriter streamWriter = fileInfo.CreateText())
                {
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"             Key - Generated on {DateTime.Now.ToShortDateString()}            ");
                    streamWriter.WriteLine("--------------------------------------------------------------------------------");
                    streamWriter.WriteLine($"Key: {key}");

                }

            }
            catch (Exception)
            {

                throw new Exception("An error occurred while crating the file");
            }

        }
    }
}
