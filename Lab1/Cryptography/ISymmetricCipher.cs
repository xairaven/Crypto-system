namespace Lab1.Cryptography;

public interface ISymmetricCypher
{
    public string Encrypt(string message, int key);

    public string Decrypt(string message, int key);
}