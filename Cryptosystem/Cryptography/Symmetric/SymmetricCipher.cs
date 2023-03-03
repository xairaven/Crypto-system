using System.Collections.Generic;

namespace Cryptosystem.Cryptography.Symmetric;

public abstract class SymmetricCipher
{
    protected const int UnicodeCardinal = 1114112;

    protected readonly List<char> EscapeSequence = new List<char>()
    {
        '\a', '\b', '\f', '\n', '\r', '\t', '\v'
    };

    public abstract string Encrypt(string message, params object[] keys);

    public abstract string Decrypt(string message, params object[] keys);
}