using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace SecurityHelpers.Extensions
{
    public static class KeyExtensions
    {
        public static string RSAParameterToString(this RSAParameters key)
        {
            StringWriter sw = new StringWriter();
            //we need a serializer
            XmlSerializer xs = new XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, key);
            //get the string from the stream
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(sw.ToString()));
        }

        public static RSAParameters ToRSAParameter(this string key)
        {
            key = Encoding.UTF8.GetString(Convert.FromBase64String(key));
            //get a stream from the string
            StringReader sr = new StringReader(key);
            //we need a deserializer
            XmlSerializer xs = new XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            return (RSAParameters)xs.Deserialize(sr);
        }
    }
}
