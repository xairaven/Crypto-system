using System;
using System.Linq;
using System.Text;

namespace Cryptosystem.Cryptography.Symmetric;

public class Trithemius : Caesar
{
    public override string Encrypt(string message, params object[] keys)
    {
        Func<int, int> handler = ValidateAndGetHandler(message, keys);

        return TrithemiusCipher(message, handler);
    }

    public override string Decrypt(string message, params object[] keys)
    {
        Func<int, int> handler = ValidateAndGetHandler(message, keys);
        
        return TrithemiusCipher(message, DecryptionHandler(handler));
    }
    
    private string TrithemiusCipher(string message, Func<int, int> handler)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < message.Length; i++)
        {
            char c = message[i];
            
            if (EscapeSequence.Contains(c))
            {
                sb.Append(c);
                continue;
            }
            
            sb.Append((char) ((c + handler(i)) % UnicodeCardinal));
        }
        
        return sb.ToString();
    }

    private Func<int, int> LinearHandler(int a, int b)
    {
        return (int position) => a * position + b;
    }

    private Func<int, int> NonLinearHandler(int a, int b, int c)
    {
        return (int position) => (a * position * position + b * position + c);
    }

    private Func<int, int> MottoHandler(string message, string motto)
    {
        return (int position) =>
        {
            if (message.Length <= motto.Length) return motto[position];

            var factor = (int) Math.Ceiling((decimal) message.Length / motto.Length);

            var localMotto = string.Concat(Enumerable.Repeat(motto, factor));

            return localMotto[position];
        };
    }
    
    private Func<int, int> ValidateAndGetHandler(string message, params object[] keys)
    {
        switch (keys.Length)
        {
            case 1:
            {
                if (keys[0] is not string motto)
                    throw new ArgumentException("Wrong arguments! Operation ByMotto failed.");

                return MottoHandler(message, motto);
            }
            case 2:
            {
                if (keys[0] is not int a
                    || keys[1] is not int b)
                    throw new ArgumentException("Wrong arguments! Operation LinearEquation failed.");

                return LinearHandler(a, b);
            }
            case 3:
            {
                if (keys[0] is not int a
                    || keys[1] is not int b
                    || keys[2] is not int c)
                    throw new ArgumentException("Wrong arguments! Operation NonLinearEquation failed.");

                return NonLinearHandler(a, b, c);
            }
            default:
                throw new ArgumentException("Wrong arguments! Validation failed!");
        }
    }

    private Func<int, int> DecryptionHandler(Func<int, int> handler)
    {
        return (int position) => -1 * handler(position);
    }
}