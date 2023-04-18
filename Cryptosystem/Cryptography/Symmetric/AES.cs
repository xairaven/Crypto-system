namespace Cryptosystem.Cryptography.Symmetric;

/// <summary>
/// keys[0] - Hex string or Base64
/// keys[1] - Cipher mode
/// keys[2] - Key
/// keys[3] - IV
/// </summary>
public class AES : SymmetricCipher, IStandardCipher
{
    private const CipherEnum CipherType = CipherEnum.AES;

    public override string Encrypt(string message, params object[] keys)
    {
        IStandardCipher cipher = this;
        return cipher.Encrypt(CipherType, message, keys);
    }

    public override string Decrypt(string message, params object[] keys)
    {
        IStandardCipher cipher = this;
        return cipher.Decrypt(CipherType, message, keys);
    }
}