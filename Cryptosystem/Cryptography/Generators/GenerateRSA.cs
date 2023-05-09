using System.Security.Cryptography;

namespace Cryptosystem.Cryptography.Generators;

public class GenerateRSA
{
    public static (string PublicKeyPEM, string PrivateKeyPEM) PEMKeys(int keySize = 2048)
    {
        using var rsa = RSA.Create(keySize);
        return (
            rsa.ExportRSAPublicKeyPem(),
            rsa.ExportPkcs8PrivateKeyPem()
        );
    }
}