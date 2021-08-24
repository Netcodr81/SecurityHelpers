using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SecurityHelpers.Helpers
{
    public static class CertificateGenerator
    {
        public static X509Certificate2 GenerateCertificate(string subject, string company, int numberOfYearsValid = 1)
        {

            SecureRandom random = new SecureRandom();
            X509V3CertificateGenerator certificateGenerator = new X509V3CertificateGenerator();

            BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            certificateGenerator.SetSerialNumber(serialNumber);

            certificateGenerator.SetIssuerDN(new X509Name($"C=NL, O={company}, CN={subject}"));
            certificateGenerator.SetSubjectDN(new X509Name($"C=NL, O={company}, CN={subject}"));
            certificateGenerator.SetNotBefore(DateTime.UtcNow.Date);
            certificateGenerator.SetNotAfter(DateTime.UtcNow.Date.AddYears(numberOfYearsValid));

            const int strength = 2048;
            KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(random, strength);
            RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);

            AsymmetricCipherKeyPair subjectKeyPair = keyPairGenerator.GenerateKeyPair();
            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;
            const string signatureAlgorithm = "SHA256WithRSA";
            Asn1SignatureFactory signatureFactory = new Asn1SignatureFactory(signatureAlgorithm, issuerKeyPair.Private);
            Org.BouncyCastle.X509.X509Certificate bouncyCert = certificateGenerator.Generate(signatureFactory);

            // Lets convert it to X509Certificate2
            X509Certificate2 certificate;

            Pkcs12Store store = new Pkcs12StoreBuilder().Build();
            store.SetKeyEntry($"{subject}_key", new AsymmetricKeyEntry(subjectKeyPair.Private), new[] { new X509CertificateEntry(bouncyCert) });
            string exportpw = Guid.NewGuid().ToString("x");

            using (MemoryStream ms = new MemoryStream())
            {
                store.Save(ms, exportpw.ToCharArray(), random);
                certificate = new X509Certificate2(ms.ToArray(), exportpw, X509KeyStorageFlags.Exportable);
            }

            return certificate;
        }

    }
}

