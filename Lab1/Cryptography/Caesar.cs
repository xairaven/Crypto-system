using System.Text;

namespace Lab1.Cryptography;

public class Caesar : SymmetricCipher
{
    public override string Encrypt(string message, int key)
    {
        return CaesarCipher(message, key);
    }

    public override string Decrypt(string message, int key)
    {
        return CaesarCipher(message, -key);
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
}