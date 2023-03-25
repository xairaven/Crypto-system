using System;
using System.Text;

namespace Cryptosystem.Cryptography.Symmetric;

public class XORCipher : SymmetricCipher
{
    public override string Encrypt(string message, params object[] keys)
    {
        var seed = Validate(keys);

        return Cipher(message, seed);
    }

    public override string Decrypt(string message, params object[] keys)
    {
        var seed = Validate(keys);

        return Cipher(message, seed);
    }

    private string Cipher(string message, int seed)
    {
        var random = new Random(seed);

        var sb = new StringBuilder();
        foreach (var с in message)
        {
            var gamma = (char) random.Next(1, UnicodeCardinal + 1);
            sb.Append((char) (с ^ gamma));
        }

        return sb.ToString();
    }

    private int Validate(params object[] keys)
    {
        if (keys.Length != 1) throw new Exception("Wrong args");
        if (keys[0] is not int key) throw new Exception("Key must be integer");

        return key;
    }
}