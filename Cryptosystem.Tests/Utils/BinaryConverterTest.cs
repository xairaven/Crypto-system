using Cryptosystem.Utils;

namespace Cryptosystem.Tests.Utils;

public class BinaryConverterTest
{
    [Fact]
    public void TestStringToBinary1()
    {
        // Arrange
        const string input = "BAG";
        const string expected = "000000000100001000000000010000010000000001000111";

        // Act
        var result = BinaryConverter.StringToBinary(input, 16);

        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestStringToBinary2()
    {
        // Arrange
        const string input = "BAG";
        const string expected = "100001010000011000111";

        // Act
        var result = BinaryConverter.StringToBinary(input, 7);

        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestBinaryToString1()
    {
        // Arrange
        const string input = "000000000100001000000000010000010000000001000111";
        const string expected = "BAG";

        // Act
        var result = BinaryConverter.BinaryToString(input, 16);

        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestBinaryToString2()
    {
        // Arrange
        const string expected = "BAG";
        const string input = "100001010000011000111";

        // Act
        var result = BinaryConverter.BinaryToString(input, 7);

        // Result
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestBinaryToString_WithPadding()
    {
        // Arrange
        const string expected = "BAG";
        const string input = "1000010100000110001110000";

        // Act
        var result = BinaryConverter.BinaryToString(input, 7);

        // Result
        Assert.Equal(expected, result);
    }
}