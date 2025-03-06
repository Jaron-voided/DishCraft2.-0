using System.Security.Claims;
using API.Controllers;
using Application.Core;
using Application.DTOs;
using Application.EntityHandlers.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DishCraft.Tests.UnitTests.Controller;

[TestFixture]
public class IngredientsControllerTests
{
    private IngredientsController _controller;
    private Mock<IMediator> _mediator;
    private Mock<ILogger<IngredientsController>> _logger;

    [SetUp]
    public void Setup()
    {
        _mediator = new Mock<IMediator>();
        _logger = new Mock<ILogger<IngredientsController>>();
        _controller = new IngredientsController(_logger.Object);

        // Setup controller context for auth
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Test]
    public async Task CreateIngredient_ValidData_ReturnsCreatedAtAction()
    {
        // Arrange
        var ingredientDto = new IngredientDto
        {
            Name = "Test Ingredient",
            Category = "Spice",
            MeasuredIn = "Weight",
            WeightUnit = "Grams",
            PricePerPackage = 5.99m,
            Quantity = 500
        };

        _mediator.Setup(m => m.Send(It.IsAny<Create.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IngredientDto>.Success(ingredientDto));

        // Act
        var result = await _controller.CreateIngredient(ingredientDto);

        // Assert
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.That(createdAtActionResult, Is.Not.Null);
        Assert.That(createdAtActionResult.Value, Is.Not.Null);
        Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(IngredientsController.CreateIngredient)));
    }

    [Test]
    public async Task CreateIngredient_InvalidData_ReturnsBadRequest()
    {
        // Arrange
        var ingredientDto = new IngredientDto { /* missing required fields */ };

        _mediator.Setup(m => m.Send(It.IsAny<Create.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IngredientDto>.Failure("Validation failed"));

        // Act
        var result = await _controller.CreateIngredient(ingredientDto);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
    }
}