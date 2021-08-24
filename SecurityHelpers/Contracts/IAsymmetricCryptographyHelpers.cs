using System;
using System.Linq;

namespace SecurityHelpers.Contracts
{
    public interface IAsymmetricCryptographyHelpers
    {
        string Decrypt(string data, string privateKey);
        string Encrypt(string data, string publicKey);
        void ExportKeysToFile((string, string) keys, string filePath, string filename = "keys");
        void GenerateAndExportKeysToFile(string filePath, string filename = "keys");
        (string publicKey, string privateKey) GenerateKeys(int keyLength = 2048);
    }
}
