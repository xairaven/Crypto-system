namespace Cryptosystem.Algorithms;

public class Euclid
{
    public static int GCD(int p, int q)
    {
        if (q == 0) return p;

        var r = p % q;
        return GCD(q, r);
    }
}