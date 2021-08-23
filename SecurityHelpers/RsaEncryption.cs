using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecurityHelpers
{
    public class RsaEncryption
    {

        public RsaEncryption()
        {

        }



        public (RSAParameters publicKey, RSAParameters privateKey) GenerateKeys(int keyLength = 2048)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = keyLength;
                return (publicKey: rsa.ExportParameters(includePrivateParameters: false), privateKey: rsa.ExportParameters(includePrivateParameters: true));
            }

        }

    }
}
