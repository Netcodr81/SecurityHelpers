using System;
using System.Linq;

namespace SecurityHelpers.Contracts
{
    public interface IHashingHelpers
    {
        string ComputeHash(string data);
        string ComputeHmac256(string key, string data);
    }
}
