namespace Cryptosystem.Algorithms;

public static class Euclid
{
    public static long GCD(long p, long q)
    {
        if (q == 0) return (int) p;

        var r = p % q;
        return GCD(q, r);
    }
    
    public static long ModularReverse(long a, long mod)
    {
        for (var x = 1; x < mod; x++)
        {
            if (((a % mod) * (x % mod)) % mod == 1) return x;
        }
            
        return 1;
    }
}