using System;
using System.Text;

namespace Cryptosystem.Cryptography.Symmetric;

public class XORCipher : SymmetricCipher
{
    public override string Encrypt(string message, params object[] keys)
    {
        return ValidateAndReturn(message, keys);
    }

    public override string Decrypt(string message, params object[] keys)
    {
        return ValidateAndReturn(message, keys);
    }

    private string CipherSeed(string message, int seed)
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
    
    private string CipherGamma(string message, string gamma)
    {
        if (gamma.Length != message.Length)
            throw new Exception("Message and pad lengths must be equal");
        
        var sb = new StringBuilder();

        for (var i = 0; i < message.Length; i++)
        {
            sb.Append((char) ((message[i] ^ gamma[i]) % UnicodeCardinal));
        }

        return sb.ToString();
    }

    private string ValidateAndReturn(string message, params object[] keys)
    {
        if (keys.Length != 1) throw new Exception("Wrong args");
        
        return keys[0] switch
        {
            int seed => CipherSeed(message, seed),
            string gamma => CipherGamma(message, gamma),
            _ => throw new Exception("Wrong key type")
        };
    }
}