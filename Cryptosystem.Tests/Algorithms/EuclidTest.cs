using Cryptosystem.Algorithms;

namespace Cryptosystem.Tests.Algorithms;

public class EuclidTest
{
    [Fact]
    public void TestGCD1()
    {
        // Arrange
        const int a = 122;
        const int b = 10;
        const int expected = 2;
        
        // Act
        var result = Euclid.GCD(a, b);
        
        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestGCD2()
    {
        Assert.Equal(1, Euclid.GCD(133, 5));
    }
}