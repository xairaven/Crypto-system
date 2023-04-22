namespace Cryptosystem.Cryptography.Base;

public interface IBruteforce
{
    public string Hack(string message, params object[] keys);
}