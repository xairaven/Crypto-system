using System;
using System.Linq;
using System.Text;
using Cryptosystem.Algorithms;
using Cryptosystem.Utils;

namespace Cryptosystem.Cryptography.Asymmetric;

public class Knapsack
{
    public string Encrypt(string message, long[] publicKey, bool isASCII)
    {
        var binary = BinaryConverter.StringToBinary(message, 
            bits: isASCII ? 7 : 16);

        var padding = new string('0', publicKey.Length - (binary.Length % publicKey.Length));
        binary = string.Concat(binary, padding);

        var bitGroupLength = publicKey.Length;

        var encrypted = new long[binary.Length / publicKey.Length];

        for (int i = 0; i < encrypted.Length; i++)
        {
            var bitGroup = binary.Substring(i * bitGroupLength, bitGroupLength);
            long sum = 0;
            
            for (int j = 0; j < bitGroupLength; j++)
            {
                var bit = (int) char.GetNumericValue(bitGroup[j]);

                if (bit == 1) sum += publicKey[j];
            }

            encrypted[i] = sum;
        }

        return string.Join(", ", encrypted);
    }

    public string Decrypt(long[] encryptedSequence, long[] secretSequence, long tInverse, long mod, bool isASCII)
    {
        var decryptedSequence = new long[encryptedSequence.Length];

        for (int i = 0; i < encryptedSequence.Length; i++)
        {
            decryptedSequence[i] = (encryptedSequence[i] * tInverse) % mod;
        }

        var sb = new StringBuilder();
        foreach (var num in decryptedSequence)
        {
            sb.Append(KnapsackAlg.GetBytes(num, secretSequence));
        }

        var result = BinaryConverter.BinaryToString(sb.ToString(),
            bits: isASCII ? 7 : 16);
        
        return result;
    }

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