using System;
using System.Text;
using System.Windows;
using Cryptosystem.Cryptography.Symmetric;

namespace Cryptosystem.Cryptography.Coding;

public class Hexadecimal : SymmetricCipher
{
    public override string Encrypt(string message, params object[] key)
    {
        var bytes = Encoding.Unicode.GetBytes(message);
        return Convert.ToHexString(bytes);
    }

    public override string Decrypt(string message, params object[] key)
    {
        try
        {
            var hexBytes = Convert.FromHexString(message);
            return Encoding.Unicode.GetString(hexBytes);
        }
        catch (FormatException exception)
        {
            MessageBox.Show(
                messageBoxText: exception.Message,
                caption: "Decrypt error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error
            );
            return message;
        }
    }
}