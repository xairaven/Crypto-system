using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptosystem.Cryptography.Generators;

public class GenerateSHA1
{
    public static string HexHash(string str)
    {
        var bytes = Encoding.Unicode.GetBytes(str);
        var hash = SHA1.HashData(bytes);

        return Convert.ToHexString(hash);
    }
}