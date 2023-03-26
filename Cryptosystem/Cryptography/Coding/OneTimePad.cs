using System;
using System.Text;

namespace Cryptosystem.Cryptography.Coding;

public class OneTimePad
{
    private const int UnicodeCardinal = 55295;

    public string Create(int padLength)
    {
        var random = new Random();

        var sb = new StringBuilder();
        for (var i = 0; i < padLength; i++)
        {
            sb.Append((char) random.Next(1, UnicodeCardinal + 1));
        }

        return sb.ToString();
    }
}