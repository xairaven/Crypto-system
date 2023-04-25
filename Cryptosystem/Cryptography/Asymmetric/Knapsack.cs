using System;
using System.Linq;
using Cryptosystem.Algorithms;

namespace Cryptosystem.Cryptography.Asymmetric;

public class Knapsack
{
    public static long[] GenerateSV(int size)
    {
        var SV = new long[size];

        for (int i = 1, N = size; i <= N; i++)
        {
            var lower = (long) ((Math.Pow(2, i - 1) - 1) * Math.Pow(2, N) + 1);
            var max = (long) (Math.Pow(2, i - 1) * Math.Pow(2, N));

            SV[i - 1] = new Random().NextInt64(lower, max + 1);
        }

        return SV;
    }
    
    public static long GenerateMod(long[] SV)
    {
        var sum = SV.Sum();

        var minValue = sum + 1;
        var maxValue = minValue + 1000;
        var mod = new Random().NextInt64(minValue, maxValue);

        return mod;
    }
    
    public static long GenerateT(long mod)
    {
        long t;
        var random = new Random();

        do
        {
            t = random.NextInt64(7, mod);
        } while (Euclid.GCD(t, mod) != 1);
        
        return t;
    }

    public static long GenerateTInverse(long t, long mod)
    {
        return Euclid.ModularReverse(t, mod);
    }

    public static long[] GeneratePV(long[] SV, long t, long mod)
    {
        var PV = new long[SV.Length];
        
        for (int i = 0; i < SV.Length; i++)
        {
            PV[i] = (SV[i] * t) % mod;
        }

        return PV;
    }
}