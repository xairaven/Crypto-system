using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cryptosystem.Enum;

using SysAES = System.Security.Cryptography.Aes;
using SysDES = System.Security.Cryptography.DES;
using SysTripleDES = System.Security.Cryptography.TripleDES;

namespace Cryptosystem.Cryptography.Symmetric;

/// <summary>
/// keys[0] - Hex string or Base64
/// keys[1] - Cipher mode
/// keys[2] - Key
/// keys[3] - IV
/// </summary>
public interface IStandardCipher
{
    public string Encrypt(CipherEnum type, string message, params object[] keys)
    {
        var provider = CryptoProvider(type, keys);

        var encryptor = provider.CreateEncryptor(provider.Key, provider.IV);

        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        
        using (var writer = new StreamWriter(cryptoStream))
        {
            writer.Write(message);
        }

        var encrypted = memoryStream.ToArray();

        cryptoStream.Close();
        memoryStream.Close();

        var dataMode = ValidateCastDataMode(keys[0]);
        
        return dataMode is DataMode.Base64 ? 
            Convert.ToBase64String(encrypted) 
            : Convert.ToHexString(encrypted);
    }

    public string Decrypt(CipherEnum type, string message, params object[] keys)
    {
        var provider = CryptoProvider(type, keys);

        var dataMode = ValidateCastDataMode(keys[0]);
        
        var bytes = dataMode is DataMode.Base64 ? 
            Convert.FromBase64String(message) 
            : Convert.FromHexString(message);

        var decryptor = provider.CreateDecryptor(provider.Key, provider.IV);

        var memoryStream = new MemoryStream(bytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        string plaintext;
        using (var reader = new StreamReader(cryptoStream))
        {
            plaintext = reader.ReadToEnd();
        }

        cryptoStream.Close();
        memoryStream.Close();

        return plaintext;
    }

    private SymmetricAlgorithm CryptoProvider(CipherEnum type, object[] keys)
    {
        if (keys.Length < 3) throw new ArgumentException("Not enough arguments!");
        
        SymmetricAlgorithm cipher = type switch
        {
            CipherEnum.AES => SysAES.Create(),
            CipherEnum.DES => SysDES.Create(),
            CipherEnum.TripleDES => SysTripleDES.Create(),
            
            _ => throw new ArgumentException("Wrong cipher type")
        };
        
        cipher.Mode = ValidateCastCipherMode(keys[1], keys.Length);

        cipher.Key = Encoding.UTF8.GetBytes(ValidateCastKey(keys[2], type));
        cipher.IV = Encoding.UTF8.GetBytes(ValidateCastIV(keys[3]));

        if (type is CipherEnum.DES && cipher.Mode is CipherMode.CFB) cipher.FeedbackSize = 8;
        
        return cipher;
    }

    private DataMode ValidateCastDataMode(object m)
    {
        // Hex or Base64
        if (m is not DataMode mode) throw new ArgumentException("Wrong data mode");

        return mode;
    }

    private CipherMode ValidateCastCipherMode(object m, int length)
    {
        // Cipher mode
        if (m is not CipherMode mode) throw new ArgumentException("Wrong cipher mode");

        return mode;
    }

    private string ValidateCastKey(object m, CipherEnum cipher)
    {
        if (m is not string key) throw new ArgumentException("Wrong key type");
        
        var bytes = Encoding.UTF8.GetByteCount(key);

        switch (cipher)
        {
            case CipherEnum.DES when bytes != 8:
            case CipherEnum.AES when !new[] {16, 24, 32}.Contains(bytes):
            case CipherEnum.TripleDES when !new[] {16, 24}.Contains(bytes):
                throw new ArgumentException("Bad key size.");
        }

        switch (cipher)
        {
            case CipherEnum.DES when SysDES.IsWeakKey(Encoding.UTF8.GetBytes(key)):
            case CipherEnum.TripleDES when SysTripleDES.IsWeakKey(Encoding.UTF8.GetBytes(key)):
                throw new ArgumentException("Weak key.");
        }

        return key;
    }
    
    private string ValidateCastIV(object m)
    {
        if (m is not string IV) throw new ArgumentException("Wrong IV type");
        
        return IV;
    }
}