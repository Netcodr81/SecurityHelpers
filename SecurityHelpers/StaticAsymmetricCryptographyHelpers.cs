using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityHelpers
{
    public static class StaticAsymmetricCryptographyHelpers
    {
        public static string Encrypt(string text, RSA rsa)
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

        public static string Decrypt(string text, RSA rsa)
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

        public static RSA CreateRsaPublicKey(X509Certificate2 certificate)
        {
            RSA publicKeyProvider = certificate.GetRSAPublicKey();
            return publicKeyProvider;
        }

        public static RSA CreateRsaPrivateKey(X509Certificate2 certificate)
        {
            RSA privateKeyProvider = certificate.GetRSAPrivateKey();
            return privateKeyProvider;
        }
    }
}
