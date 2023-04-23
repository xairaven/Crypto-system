using System;
using System.Data;

namespace Cryptosystem.Algorithms;

public static class KnapsackAlg
{
    public static string GetBytes(int S, int[] sequence)
    {
        if (!IsSorted(sequence)) Array.Sort(sequence);

        var result = "";
        for (int i = sequence.Length - 1; i >= 0; i--)
        {
            if (sequence[i] <= S)
            {
                S -= sequence[i];
                result = "1" + result;
            }
            else
            {
                result = "0" + result;
            }
        }

        if (S != 0) throw new EvaluateException("Problem is not solved!");
        
        return result;
    }
    
    private static bool IsSorted(int[] sequence)
    {
        for (int i = 1; i < sequence.Length; i++)
        {
            if (sequence[i] < sequence[i - 1]) return false;
        }

        return true;
    }
}