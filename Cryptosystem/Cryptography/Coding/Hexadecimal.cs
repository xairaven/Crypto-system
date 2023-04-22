using System;
using System.Text;
using System.Windows;
using Cryptosystem.Cryptography.Base;

namespace Cryptosystem.Cryptography.Coding;

public class Hexadecimal : ISymmetric
{
    public string Encrypt(string message, params object[] key)
    {
        var bytes = Encoding.Unicode.GetBytes(message);
        return Convert.ToHexString(bytes);
    }

    public string Decrypt(string message, params object[] key)
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