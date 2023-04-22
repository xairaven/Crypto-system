using System;
using System.Text;
using Cryptosystem.Cryptography.Base;

namespace Cryptosystem.Cryptography.Symmetric;

public class Caesar : ISymmetric
{
    public string Encrypt(string message, params object[] keys)
    {
        var key = Validate(keys);
        
        return CaesarCipher(message, key);
    }

    public string Decrypt(string message, params object[] keys)
    {
        var key = Validate(keys);
        
        return CaesarCipher(message, -key);
    }

    private string CaesarCipher(string message, int key)
    {
        var sb = new StringBuilder();

        foreach (var c in message)
        {
            sb.Append((char) ((c + key) % Constants.UnicodeCardinal));
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