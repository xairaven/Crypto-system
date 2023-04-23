using Cryptosystem.Algorithms;

namespace Cryptosystem.Tests.Algorithms;

public class KnapsackAlgTest
{
    [Fact]
    public void TestAlg1()
    {
        // Arrange
        const int sum = 1161; 
        var sequence = new int[] {43, 129, 215, 473, 903};

        var expected = "10101";
        
        // Act
        var result = KnapsackAlg.GetBytes(sum, sequence);
        
        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestAlg2()
    {
        var sequence = new int[] {43, 129, 215, 473, 903};
        const int sum = 43;
        const string expected = "10000";
        Assert.Equal(expected, KnapsackAlg.GetBytes(sum, sequence));
    }
    
    [Fact]
    public void TestAlg3()
    {
        var sequence = new int[] {43, 129, 215, 473, 903};
        const int sum = 1376;
        const string expected = "00011";
        Assert.Equal(expected, KnapsackAlg.GetBytes(sum, sequence));
    }
    
    [Fact]
    public void TestAlg4()
    {
        var sequence = new int[] {2, 3, 6, 13, 27, 52};
        const int sum = 70;
        const string expected = "110101";
        Assert.Equal(expected, KnapsackAlg.GetBytes(sum, sequence));
    }
}