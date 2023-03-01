using System.Collections.Generic;

namespace Lab1.Cryptography;

public abstract class SymmetricCipher
{
    protected const int UnicodeCardinal = 1114112;

    protected readonly List<char> EscapeSequence = new List<char>()
    {
        '\n',
        '\f',
        '\r',
    };

    public abstract string Encrypt(string message, int key);

    public abstract string Decrypt(string message, int key);
}