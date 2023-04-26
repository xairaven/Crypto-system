using Cryptosystem.Cryptography.Asymmetric;

namespace Cryptosystem.Tests.KnapsackTests;

public class GenerateSecretTest
{
    [Fact]
    public void TestSV1()
    {
        // Arrange
        var sv = Knapsack.GenerateSV(10);
        var expected = true;
        
        // Act
        var result = IsSorted(sv);
        
        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestSV2()
    {
        // Arrange
        var sv = Knapsack.GenerateSV(10);
        var expected = true;
        
        // Act
        var result = IsSuperIncreasing(sv);
        
        // Result
        Assert.Equal(expected, result);
    }

    private bool IsSorted(long[] sequence)
    {
        for (int i = 1; i < sequence.Length; i++)
        {
            if (sequence[i] < sequence[i - 1]) return false;
        }

        return true;
    }

    private bool IsSuperIncreasing(long[] sequence)
    {
        for (int i = 1; i < sequence.Length; i++)
        {
            if (sequence[i] < sequence[0..i].Sum()) return false;
        }

        return true;
    }
}