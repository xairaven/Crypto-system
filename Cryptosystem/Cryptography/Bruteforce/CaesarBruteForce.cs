using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cryptosystem.Cryptography.Symmetric;

namespace Cryptosystem.Cryptography.Bruteforce;

public class CaesarBruteForce : SymmetricCipher
{
    private readonly List<string> _bruteForceDictionary;
    
    public CaesarBruteForce(List<string> dict)
    {
        _bruteForceDictionary = dict;
    }

    public override string Decrypt(string message, params object[] keys)
    {
        var key = Validate(keys);

        bool done = false;
        
        var sb = new StringBuilder();
        sb.Append("Initial message:\n")
            .Append(message + "\n\n");
        
        for (int i = key; i >= -key; i--)
        {
            var hackedMessage = CaesarCipher(message, i);
            foreach (var word in _bruteForceDictionary)
            {
                if (!hackedMessage.Contains(word, StringComparison.InvariantCultureIgnoreCase)) continue;
                
                sb.Append($"Key {-i}: \t")
                    .Append(hackedMessage)
                    .Append('\n');

                done = true;
                break;
            }
        }

        if (!done) sb.Append("No matches found.");
        return sb.ToString();
    }
    
    private string CaesarCipher(string message, int key)
    {
        var sb = new StringBuilder();

        foreach (var c in message)
        {
            if (EscapeSequence.Contains(c))
            {
                sb.Append(c);
                continue;
            }

            sb.Append((char) ((c + key) % UnicodeCardinal));
        }
        
        return sb.ToString();
    }
    
    
    public override string Encrypt(string message, params object[] keys)
    {
        throw new NotImplementedException();
    }
    
    private int Validate(params object[] keys)
    {
        if (keys.Length != 1) throw new Exception("Wrong args");
        if (keys[0] is not int key) throw new Exception("Key must be integer");

        if (key > UnicodeCardinal) key = UnicodeCardinal;
        
        return key;
    }
}