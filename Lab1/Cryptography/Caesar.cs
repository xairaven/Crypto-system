using System.Text;
using System.Windows;

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
        if (!Validate(key)) return message;

        var sb = new StringBuilder();

        foreach (var c in message)
        {
            if (EscapeSequence.Contains(c))
            {
                sb.Append(c);
                continue;
            }

            sb.Append((char) ((c + key) % UnicodeCardinal) );
        }
        
        return sb.ToString();
    }

    private bool Validate(int key)
    {
        if (key != 0) return true;
       
        MessageBox.Show(
            messageBoxText: "Key must be a number (or unequal to 0)",
            caption: "Error",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error
        );
        return false;
    }
}