namespace Cryptosystem.Algorithms;

public static class Euclid
{
    public static int GCD(int p, int q)
    {
        if (q == 0) return p;

        var r = p % q;
        return GCD(q, r);
    }
    
    public static int ModularReverse(int a, int mod)
    {
        for (var x = 1; x < mod; x++)
        {
            if (((a % mod) * (x % mod)) % mod == 1) return x;
        }
            
        return 1;
    }
}