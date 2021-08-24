using System;
using System.Linq;

namespace SecurityHelpers.Contracts
{
    public interface ISymmetricCryptographyHelper
    {
        string DecryptString(string dataToDecrypt, string iv, string key);
        string EncryptString(string dataToEncrypt, string iv, string key);

        string GenerateInitialVector();
        string GenerateSymmetricKey();

        void GenerateAndExportKeysToFile(string filePath, string filename = "key");

        void ExportKeyToFile(string key, string filePath, string filename = "key");
    }
}
