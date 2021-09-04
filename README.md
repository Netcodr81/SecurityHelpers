# SecurityHelpers
A class library that has classes to encrypt strings symmetrically and asymmetrically. Can be used in C# and VB projects

## Getting Started
You can install the latest package via Nuget package manager just search for *SecurityHelpers*. You can also install via powershell using the following command.

```powershell
Install-Package SecurityHelpers -Version 1.2.0
```
Or via the donet CLI

```bash
dotnet add package SecurityHelpers --version 1.2.0
```

## Using the package

### Symetric Encryption Helpers
1. Create an instance of the SymetricCryptographyHelper class
```csharp
SymmetricCryptographyHelper symmetricHelper = new SymmetricCryptographyHelper();
```

2. Using the instance of the SymmetricCryptographyHelper created above, create the initial vector and key
```csharp
string key =  symmetricHelper.GenerateSymmetricKey();
string iv = symmetricHelper.GenerateInitialVector();
```
3. Now that you have the key and initial vector, you can encrypt the text using the *EncryptString* method
```csharp
string originalText = "Original symmetric text";
string encryptedText = symmetricHelper.EncryptString(originalText, iv, key);
```
4. To decrypt the text, use the *DecryptString* passing in the same key and initial vector used to encrypt the string
```csharp
string decryptedString = symmetricHelper.DecryptString(encryptedText, iv, key);
```
5. If the correct key and intial vector are used, the encrypted text will be returned. If not, an empty string is returned.

There are two other methods on the SymmetricCryptographyHelper class, *GenerateAndExportKeysToFile* and *ExportKeyToFile*
```csharp

SymmetricCryptographyHelper symmetricHelper = new SymmetricCryptographyHelper();
symmetricHelper.GenerateAndExportKeysToFile(string filePath, string fileName); //This will generate and export the key and initial vector to a text file

symmetricHelper.ExportKeyToFile(string key, string filePath, string fileName); // This will export the key to a  text file
```

### Asymmetric Encryption Helpers
1. Create an instance of the AsymmetricCryptographyHelpers class
```csharp
 AsymmetricCryptographyHelpers asymmetricHelper = new AsymmetricCryptographyHelpers();
 ```
 2. Using the instance created above, generate the private and public keys
 ```csharp
  (string publicKey, string privateKey) encryptionKeys = asymmetricHelper.GenerateKeys();
   string publicKey = encryptionKeys.publicKey;
   string privateKey = encryptionKeys.privateKey;
 ```
 3. Use the public key to encrypt the text
 ```csharp
 string originalText = "Original asymmetric text";
 string encryptedText = asymmetricHelper.Encrypt(originalText, publicKey);
 ```
 4. To decrypt the text use the private key generated in step 2
 ```csharp
 string decryptedText = asymmetricHelper.Decrypt(encryptedText, privateKey);
```
5. If the private key used is correct, the decrypted text will be returned. If a wrong key is used, an empty string is returned.

There are two other methods on the AsymmetricCryptographyHelpers class, *GenerateAndExportKeysToFile* and *ExportKeyToFile*
```csharp

AsymmetricCryptographyHelpers asymmetricHelper = new AsymmetricCryptographyHelpers();
asymmetricHelper.GenerateAndExportKeysToFile(string filePath, string fileName); //This will generate and export the public and private keys to a text file

asymmetricHelper.ExportKeyToFile((string, string) keys, string filePath, string filename); // This will export the private and public keys to a  text file
```
### Hashing Helpers
1. Create an instance of the HashingHelpers class
```csharp
HashingHelpers hashHelper = new HashingHelpers();
```
2. Use the instance created above to hash a string
```csharp
string textToHash = "This is a sentence to hash";
string hashedText = hashHelper.ComputeHash(textToHash);
```
3. If you want to create an HMAC hash instance you can use the *ComputeHmac256* method on the HashingHelpers class
```csharp
string textToHash = "This is a sentence to hash";
string key = "hashKey"; // Obviously you want to use a stronger key. This is just for demo purposes.
string hashedText = hashHelper.ComputeHmac256(key, textToHash);
```
3. You can generate a random key using the *GetEncodedRandomNumberString* method on the StaticHelpers class
```csharp
string key = StaticHelpers.GetEncodedRandomNumberString(int keyLength);
```
