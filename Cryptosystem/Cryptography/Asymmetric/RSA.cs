using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptosystem.Cryptography.Asymmetric;

public class LocalRSA
{
    private readonly RSAEncryptionPadding _padding;

    private readonly RSAEncryptionPadding DefaultPadding = RSAEncryptionPadding.OaepSHA256;
    
    public LocalRSA(RSAEncryptionPadding? padding = null)
    {
        _padding = padding ?? DefaultPadding;
    }

    public string Encrypt(string message, string publicKey)
    {
        using var provider = RSA.Create();
        
        provider.ImportFromPem(publicKey);

        var messageBytes = Encoding.Unicode.GetBytes(message);
        var encryptedBytes = provider.Encrypt(messageBytes, _padding);

        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedMessage, string privateKey)
    {
        using var provider = RSA.Create();
        
        provider.ImportFromPem(privateKey);

        var encryptedBytes = Convert.FromBase64String(encryptedMessage);
        var decryptedBytes = provider.Decrypt(encryptedBytes, _padding);

        return Encoding.Unicode.GetString(decryptedBytes);
    }
}