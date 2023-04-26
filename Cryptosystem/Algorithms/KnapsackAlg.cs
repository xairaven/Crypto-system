using System;
using System.Data;

namespace Cryptosystem.Algorithms;

public static class KnapsackAlg
{
    public static string GetBytes(long sum, long[] sequence)
    {
        if (!IsSorted(sequence)) throw new ArgumentException("Sequence is not sorted");

        var result = "";
        for (int i = sequence.Length - 1; i >= 0; i--)
        {
            if (sequence[i] <= sum)
            {
                sum -= sequence[i];
                result = "1" + result;
            }
            else
            {
                result = "0" + result;
            }
        }

        if (sum != 0) throw new EvaluateException("Knapsack problem is not solved!");
        
        return result;
    }
    
    private static bool IsSorted(long[] sequence)
    {
        for (int i = 1; i < sequence.Length; i++)
        {
            if (sequence[i] < sequence[i - 1]) return false;
        }

        return true;
    }
}