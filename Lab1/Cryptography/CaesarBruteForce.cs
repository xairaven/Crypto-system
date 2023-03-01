using System;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace Lab1.Cryptography;

public class CaesarBruteForce : SymmetricCipher
{
    public override string Decrypt(string message, params object[] keys)
    {
        var key = Validate(keys);
        
        var sb = new StringBuilder();
        sb.Append("Initial message:\n")
            .Append(message + "\n\n");

        var stopwatch = new Stopwatch();
        for (int i = -key; i < key; i++)
        {
            sb.Append($"Key {i}: \t")
                .Append(CaesarCipher(message, i))
                .Append('\n');
        }

        stopwatch.Stop();
        
        MessageBox.Show(
            messageBoxText: $"BruteForce was done in {stopwatch.Elapsed} ms.",
            caption: "Elapsed time",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information);

        
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
        throw new System.NotImplementedException();
    }
    
    private int Validate(params object[] keys)
    {
        if (keys.Length != 1) throw new Exception("Wrong args");
        if (keys[0] is not int key) throw new Exception("Key must be integer");

        if (key < UnicodeCardinal) key = UnicodeCardinal;
        
        return key;
    }
}