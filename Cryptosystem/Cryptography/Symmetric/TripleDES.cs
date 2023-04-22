using Cryptosystem.Cryptography.Base;

namespace Cryptosystem.Cryptography.Symmetric;

/// <summary>
/// keys[0] - Hex string or Base64
/// keys[1] - Cipher mode
/// keys[2] - Key
/// keys[3] - IV
/// </summary>
public class TripleDES : BlockCipher, ISymmetric
{
    private const CipherEnum CipherType = CipherEnum.TripleDES;

    public string Encrypt(string message, params object[] keys)
    {
        return base.Encrypt(CipherType, message, keys);
    }

    public string Decrypt(string message, params object[] keys)
    {
        return base.Decrypt(CipherType, message, keys);
    }
}