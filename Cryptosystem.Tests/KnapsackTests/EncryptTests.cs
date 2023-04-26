using Cryptosystem.Algorithms;
using Cryptosystem.Cryptography.Asymmetric;

namespace Cryptosystem.Tests.KnapsackTests;

public class EncryptTests
{
    [Fact]
    public void TestEncrypt1()
    {
        // Arrange
        var knapsack = new Knapsack();

        var message = "BAG";
        var publicKeySequence = new long[] { 193, 579, 965, 123, 53};
        var isASCII = true;
        
        var expected = "193, 1158, 176, 176, 193";
        
        // Act
        var result = knapsack.Encrypt(message, publicKeySequence, isASCII);
        
        // Result
        Assert.Equal(expected, result);
    }
}