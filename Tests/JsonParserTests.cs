using IncaTechnologies.Recurrence;

namespace Tests;

public class JsonParserTests
{
    [Fact]
    public void Parse_InvalidString_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidJson = "{}";
        // Act
        IRecurrent parse() => JsonParser.Parse(invalidJson);
        // Assert
        Assert.Throws<ArgumentException>(parse);
    }
    [Fact]
    public void TryParse_InvalidString_ShouldReturnFalse()
    {
        // Arrange
        var invalidJson = "{}";
        // Act
        var result = JsonParser.TryParse(invalidJson, out _);
        // Assert
        Assert.False(result);
    }
    [Fact]
    public void TryParse_ValidString_ShouldReturnTrue()
    {
        // Arrange
        var invalidJson = DefaultOccurrences.Yearly.ToJson();
        // Act
        var result = JsonParser.TryParse(invalidJson, out _);
        // Assert
        Assert.True(result);
    }
}