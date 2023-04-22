namespace Cryptosystem.Cryptography.Base;

public interface ISymmetric
{
    public abstract string Encrypt(string message, params object[] keys);

    public abstract string Decrypt(string message, params object[] keys);
}