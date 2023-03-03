using System;
using System.Text;
using System.Windows;
using Cryptosystem.Cryptography.Symmetric;

namespace Cryptosystem.Cryptography.Coding;

public class Base64 : SymmetricCipher
{
    public override string Encrypt(string message, params object[] key)
    {
        var bytes = Encoding.Unicode.GetBytes(message);
        return Convert.ToBase64String(bytes);
    }

    public override string Decrypt(string message, params object[] key)
    {
        try
        {
            var base64Bytes = Convert.FromBase64String(message);
            return Encoding.Unicode.GetString(base64Bytes);
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