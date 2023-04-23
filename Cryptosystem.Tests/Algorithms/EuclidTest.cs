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

    [Fact]
    public void TestGCD3()
    {
        Assert.Equal(1, Euclid.GCD(2000, 51));
    }
    
    [Fact]
    public void TestModularReverse1()
    {
        Assert.Equal(1451, Euclid.ModularReverse(51, 2000));
    }
    
    [Fact]
    public void TestModularReverse2()
    {
        Assert.Equal(4, Euclid.ModularReverse(3, 11));
    }
    
    [Fact]
    public void TestModularReverse3()
    {
        Assert.Equal(12, Euclid.ModularReverse(10, 17));
    }
}