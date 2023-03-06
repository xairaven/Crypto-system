using System;
using System.Linq;
using System.Text;

namespace Cryptosystem.Cryptography.Symmetric;

public class Trithemius : Caesar
{
    public override string Encrypt(string message, params object[] keys)
    {
        Func<int, int> handler = ValidateAndGetHandler(TypeOfCoding.Encrypt, message, keys);

        return TrithemiusCipher(message, handler);
    }

    public override string Decrypt(string message, params object[] keys)
    {
        Func<int, int> handler = ValidateAndGetHandler(TypeOfCoding.Decrypt, message, keys);
        
        return TrithemiusCipher(message, handler);
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

    private Func<int, int> ValidateAndGetHandler(TypeOfCoding method, string message, params object[] keys)
    {
        switch (keys.Length)
        {
            case 1:
            {
                if (keys[0] is not string motto)
                    throw new ArgumentException("Wrong arguments! Operation ByMotto failed.");

                return ByMotto(method, message, motto);
            }
            case 2:
            {
                if (keys[0] is not int a
                    || keys[1] is not int b)
                    throw new ArgumentException("Wrong arguments! Operation LinearEquation failed.");

                return LinearEquation(method, a, b);
            }
            case 3:
            {
                if (keys[0] is not int a
                    || keys[1] is not int b
                    || keys[2] is not int c)
                    throw new ArgumentException("Wrong arguments! Operation NonLinearEquation failed.");

                return NonLinearEquation(method, a, b, c);
            }
            default:
                throw new ArgumentException("Wrong arguments! Validation failed!");
        }
    }

    private Func<int, int> LinearEquation(TypeOfCoding method, int a, int b)
    {
        var factor = method == TypeOfCoding.Encrypt ? 1 : -1;

        return (int position) => factor * (a * position + b);
    }

    private Func<int, int> NonLinearEquation(TypeOfCoding method, int a, int b, int c)
    {
        var factor = method == TypeOfCoding.Encrypt ? 1 : -1;
        
        return (int position) => factor * (a * position * position + b * position + c);
    }

    private Func<int, int> ByMotto(TypeOfCoding method, string message, string motto)
    {
        var factor = method == TypeOfCoding.Encrypt ? 1 : -1;
        
        return (int position) =>
        {
            if (message.Length <= motto.Length) return motto[position];

            var multiplier = (int) Math.Ceiling((decimal) message.Length / motto.Length);

            var localMotto = string.Concat(Enumerable.Repeat(motto, multiplier));

            return factor * localMotto[position];
        };
    }
    
    private enum TypeOfCoding
    {
        Encrypt,
        Decrypt
    }
}