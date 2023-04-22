using System;
using System.Text;
using Cryptosystem.Cryptography.Base;

namespace Cryptosystem.Cryptography.Symmetric;

public class Vernam : ISymmetric
{
    public string Encrypt(string message, params object[] keys)
    {
        var gamma = Validate(message.Length, keys);
        
        return VernamCipher(Mode.Encrypt, message, gamma);
    }

    public string Decrypt(string message, params object[] keys)
    {
        var gamma = Validate(message.Length, keys);
        
        return VernamCipher(Mode.Decrypt, message, gamma);
    }

    private string VernamCipher(Mode mode, string message, string gamma)
    {
        var sb = new StringBuilder();

        for (var i = 0; i < message.Length; i++)
        {
            var symbol = (mode == Mode.Encrypt) ? 
                message[i] + gamma[i] : 
                message[i] - gamma[i];

            sb.Append((char) (symbol % Constants.UnicodeCardinal));
        }

        return sb.ToString();
    }

    private string Validate(int messageLength, params object[] keys)
    {
        if (keys.Length != 1) throw new Exception("Wrong args");
        if (keys[0] is not string gamma) throw new Exception("Key must be integer");
        if (gamma.Length != messageLength) throw new Exception("Message and pad lengths must be equal");
            
        return gamma;
    }

    private enum Mode
    {
        Encrypt,
        Decrypt
    }
}