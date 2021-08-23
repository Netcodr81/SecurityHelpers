using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecurityHelpers.BaseClasses
{
    public class NewRSA
    {
        private AsymmetricCipherKeyPair _keyPair;

        public NewRSA()
        {
            SecureRandom random = new SecureRandom();
            KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(random, 2048);
            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(keyGenerationParameters);
            _keyPair = generator.GenerateKeyPair();

        }


        public string GetPrivateKey()
        {
            return JsonConvert.SerializeObject(_keyPair.Private);
        }

        public string GetPublicKey()
        {
            return JsonConvert.SerializeObject(_keyPair.Public);
        }

        public string Encrypt(string textToEncrypt, string publicKeyJson)
        {
            AsymmetricCipherKeyPair encryptionKey = JsonConvert.DeserializeObject<AsymmetricCipherKeyPair>(publicKeyJson);

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES");
            cipher.Init(true, encryptionKey.Public);

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] encryptedData = ApplyCipher(dataToEncrypt, cipher, 2048);
            return Convert.ToBase64String(encryptedData);
        }
        public string Decrypt(string encryptedData, string privateKeyJson)
        {
            AsymmetricCipherKeyPair decryptionKey = JsonConvert.DeserializeObject<AsymmetricCipherKeyPair>(privateKeyJson);

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES");
            cipher.Init(false, decryptionKey.Private);

            int blockSize = 2048;

            byte[] dataToDecrypt = Convert.FromBase64String(encryptedData);
            byte[] decryptedData = ApplyCipher(dataToDecrypt, cipher, blockSize);
            return Encoding.UTF8.GetString(decryptedData);
        }

        private byte[] ApplyCipher(byte[] data, IBufferedCipher cipher, int blockSize)
        {
            MemoryStream inputStream = new MemoryStream(data);
            List<byte> outputBytes = new List<byte>();

            int index;
            byte[] buffer = new byte[blockSize];
            while ((index = inputStream.Read(buffer, 0, blockSize)) > 0)
            {
                byte[] cipherBlock = cipher.DoFinal(buffer, 0, index);
                outputBytes.AddRange(cipherBlock);
            }

            return outputBytes.ToArray();
        }
    }
}
