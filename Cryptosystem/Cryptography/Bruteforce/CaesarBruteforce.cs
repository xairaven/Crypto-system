﻿using System;
using System.Collections.Generic;
using System.Text;
using Cryptosystem.Cryptography.Base;

namespace Cryptosystem.Cryptography.Bruteforce;

public class CaesarBruteforce : IBruteforce
{
    private readonly List<string> _bruteForceDictionary;
    
    public CaesarBruteforce(List<string> dict)
    {
        _bruteForceDictionary = dict;
    }

    public string Hack(string message, params object[] keys)
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
            sb.Append((char) ((c + key) % Constants.UnicodeCardinal));
        }
        
        return sb.ToString();
    }

    private int Validate(params object[] keys)
    {
        if (keys.Length != 1) throw new Exception("Wrong args");
        if (keys[0] is not int key) throw new Exception("Key must be integer");

        if (key > Constants.UnicodeCardinal) key = Constants.UnicodeCardinal;
        
        return key;
    }
}