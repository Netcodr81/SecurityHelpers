using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SecurityHelpers
{
    public class AsymmetricCryptographyHelpers
    {
        public AsymmetricCryptographyHelpers()
        {

        }

        public string Encrypt(string text, RSA rsa)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(text);
                byte[] cipherText = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(cipherText);
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }

        public string Decrypt(string text, RSA rsa)
        {
            try
            {
                byte[] data = Convert.FromBase64String(text);
                byte[] cipherText = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(cipherText);
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }

        public RSA CreateRsaPublicKey(X509Certificate2 certificate)
        {
            RSA publicKeyProvider = certificate.GetRSAPublicKey();
            return publicKeyProvider;
        }

        public RSA CreateRsaPrivateKey(X509Certificate2 certificate)
        {
            RSA privateKeyProvider = certificate.GetRSAPrivateKey();
            return privateKeyProvider;
        }
    }
}
