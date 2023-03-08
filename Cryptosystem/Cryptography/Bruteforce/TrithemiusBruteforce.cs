using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptosystem.Cryptography.Bruteforce;

public class TrithemiusBruteforce
{
    private readonly List<string> _dict;

    public TrithemiusBruteforce(List<string> dict)
    {
        _dict = dict;
    }
    
    public string Decrypt(string initialMessage, string encryptedMessage)
    {
        Validate(initialMessage, encryptedMessage);
        
        var diff = FindDiff(initialMessage, encryptedMessage);

        var sb = new StringBuilder();
        sb.Append("Full key:\n")
            .Append(diff);

        var possibleKeywords = new StringBuilder();
        
        foreach (var word in _dict)
        {
            if (diff.Length < word.Length || diff.Length == 0) continue;
            
            if (!diff[..word.Length]
                    .Contains(word, StringComparison.InvariantCultureIgnoreCase))
                continue;

            possibleKeywords.Append(diff[..word.Length]).Append('\n');
        }

        if (possibleKeywords.Length != 0)
        {
            sb.Append("\n\nPossible keywords (from dictionary):\n");
            sb.Append(possibleKeywords);
        }
        
        return sb.ToString();
    }

    private string FindDiff(string initialMessage, string encryptedMessage)
    {
        var length = initialMessage.Length;
        
        var diff = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            diff.Append((char) (encryptedMessage[i] - initialMessage[i]));
        }

        return diff.ToString();
    }

    private void Validate(string initialMessage, string encryptedMessage)
    {
        if (initialMessage.Length != encryptedMessage.Length)
            throw new ArgumentException("Initial and Encrypted messages are not in pair.");
    }
}