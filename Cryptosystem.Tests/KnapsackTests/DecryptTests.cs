using Cryptosystem.Cryptography.Asymmetric;

namespace Cryptosystem.Tests.KnapsackTests;

public class DecryptTests
{
    [Fact]
    public void TestDecrypt1()
    {
        // Arrange
        var knapsack = new Knapsack();

        var encryptedSequence = new long[] { 193, 1158, 176, 176, 193 };

        var secretSequence = new long[] { 43, 129, 215, 473, 903 };
        var t = 51;
        var mod = 2000;
        var tInverse = Knapsack.GenerateTInverse(t, mod);
        var isASCII = true;
        
        var expected = "BAG";
        
        // Act
        var result = knapsack.Decrypt(encryptedSequence, secretSequence, tInverse, mod, isASCII);
        
        // Result
        Assert.Equal(expected, result);
    }
}