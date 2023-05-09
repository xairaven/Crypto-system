using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptosystem.Cryptography.Asymmetric;

public class LocalRSA
{
    private readonly int _keySize;
    private readonly RSAEncryptionPadding _padding;

    public LocalRSA(int size = 2048, RSAEncryptionPadding? padding = null)
    {
        _keySize = size;
        _padding = padding ?? RSAEncryptionPadding.OaepSHA256;
    }

    public string Encrypt(string message, string publicKey)
    {
        using var provider = RSA.Create(_keySize);
        
        provider.ImportFromPem(publicKey);

        var messageBytes = Encoding.Unicode.GetBytes(message);
        var encryptedBytes = provider.Encrypt(messageBytes, _padding);

        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedMessage, string privateKey)
    {
        using var provider = RSA.Create(_keySize);
        
        provider.ImportFromPem(privateKey);

        var encryptedBytes = Convert.FromBase64String(encryptedMessage);
        var decryptedBytes = provider.Decrypt(encryptedBytes, _padding);

        return Encoding.Unicode.GetString(decryptedBytes);
    }
}