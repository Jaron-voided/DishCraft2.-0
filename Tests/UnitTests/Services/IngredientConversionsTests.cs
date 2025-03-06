using Application.Utilities;
using Xunit;
using Assert = Xunit.Assert;

namespace DishCraft.Tests.UnitTests.Services;

public class IngredientConversionsTests
{
    private readonly IngredientConversions _conversions;

    public IngredientConversionsTests()
    {
        _conversions = new IngredientConversions();
    }

    [Fact]
    public void ConvertPoundsToGrams_ReturnsCorrectValue()
    {
        // Arrange
        decimal pounds = 2;
        decimal expectedGrams = 908; // 2 * 454

        // Act
        var result = _conversions.ConvertPoundsToGrams(pounds);

        // Assert
        Assert.Equal(expectedGrams, result);
    }
    
    [Fact]
    public void ConvertCupsToMilliliters_ReturnsCorrectValue()
    {
        // Arrange
        decimal cups = 1;
        decimal expectedMl = 236.5m; // Based on your constant

        // Act
        var result = _conversions.ConvertCupsToMilliliters(cups);

        // Assert
        Assert.Equal(expectedMl, result);
    }
}