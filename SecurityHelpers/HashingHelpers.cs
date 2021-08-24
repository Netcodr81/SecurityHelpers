using SecurityHelpers.Contracts;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecurityHelpers
{
    public class HashingHelpers : IHashingHelpers
    {
        public HashingHelpers()
        {

        }

        public string ComputeHash(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(dataBytes));
            }
        }

        public string ComputeHmac256(string key, string data)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(data);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (HMACSHA256 hmac = new HMACSHA256(dataBytes))
            {
                return Convert.ToBase64String(hmac.ComputeHash(dataBytes));
            }
        }
    }
}
