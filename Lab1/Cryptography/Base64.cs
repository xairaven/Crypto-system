using System;
namespace Lab1.Cryptography;

public class Base64 : SymmetricCipher
{
    public override string Encrypt(string message, params object[] key)
    {
        var bytes = System.Text.Encoding.Unicode.GetBytes(message);
        return Convert.ToBase64String(bytes);
    }

    public override string Decrypt(string message, params object[] key)
    {
        var base64Bytes = Convert.FromBase64String(message);
        return System.Text.Encoding.Unicode.GetString(base64Bytes);
    }
}