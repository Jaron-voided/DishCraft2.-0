using Application.Core;
using Application.DTOs;
using Application.EntityHandlers.Ingredients;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Persistence;

namespace Tests.IntegrationTests.Handlers.Ingredients;

[TestFixture]
public class CreateIngredientHandlerTests
{
    private DataContext _context;
    private IMapper _mapper;
    private Mock<IUserAccessor> _userAccessor;
    private Mock<ILogger<Create.Handler>> _logger;
    private Create.Handler _handler;
    private readonly string _testUserId = "test-user-id";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        // Set up AutoMapper
        var mappingConfig = new MapperConfiguration(cfg => 
            cfg.AddProfile(new MappingProfiles()));
        _mapper = mappingConfig.CreateMapper();
    }

    [SetUp]
    public async Task Setup()
    {
        // Create new in-memory database for each test
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new DataContext(options);

        // Set up mocks
        _userAccessor = new Mock<IUserAccessor>();
        _logger = new Mock<ILogger<Create.Handler>>();
        
        // Create test user
        var user = new AppUser 
        { 
            Id = _testUserId,
            UserName = "testuser",
            Email = "test@example.com",
            DisplayName = "Test User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Set up user accessor
        _userAccessor.Setup(x => x.GetUserId())
            .Returns(_testUserId);

        // Create handler
        _handler = new Create.Handler(_context, _userAccessor.Object, _logger.Object, _mapper);
    }

    [Test]
    public async Task Handle_ValidWeightIngredient_ReturnsSuccess()
    {
        // Arrange
        var command = new Create.Command
        {
            IngredientDto = new IngredientDto
            {
                Name = "All Purpose Flour",
                Category = "Baking",
                MeasuredIn = "Weight",
                WeightUnit = "Grams",
                PricePerPackage = 3.99m,
                Quantity = 1000,
                Calories = 364,
                Carbs = 76,
                Fat = 1,
                Protein = 10
            }
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null);

        var savedIngredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.Name == "All Purpose Flour");

        Assert.That(savedIngredient, Is.Not.Null);
        Assert.That(savedIngredient.AppUserId, Is.EqualTo(_testUserId));
        Assert.That(savedIngredient.Category.ToString(), Is.EqualTo("Baking"));
        Assert.That(savedIngredient.MeasuredIn.ToString(), Is.EqualTo("Weight"));
        Assert.That(savedIngredient.WeightUnit.ToString(), Is.EqualTo("Grams"));
        
        // Use delta for decimal comparison
        Assert.That(result.Value.PricePerMeasurement, Is.EqualTo(0.00399m).Within(0.00001m));
    }

    [Test]
    public async Task Handle_ValidVolumeIngredient_ReturnsSuccess()
    {
        // Arrange
        var command = new Create.Command
        {
            IngredientDto = new IngredientDto
            {
                Name = "Whole Milk",
                Category = "Dairy",
                MeasuredIn = "Volume",
                VolumeUnit = "Milliliters",
                PricePerPackage = 4.99m,
                Quantity = 2000,
                Calories = 60,
                Carbs = 5,
                Fat = 3,
                Protein = 3
            }
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null);
        
        var savedIngredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.Name == "Whole Milk");
        Assert.That(savedIngredient.MeasuredIn.ToString(), Is.EqualTo("Volume"));
    }

    [Test]
    public async Task Handle_ValidEachIngredient_ReturnsSuccess()
    {
        // Arrange
        var command = new Create.Command
        {
            IngredientDto = new IngredientDto
            {
                Name = "Large Eggs",
                Category = "Dairy",
                MeasuredIn = "Each",
                PricePerPackage = 5.99m,
                Quantity = 12,
                Calories = 70,
                Carbs = 0,
                Fat = 5,
                Protein = 6
            }
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value.PricePerMeasurement, Is.EqualTo(0.49916m).Within(0.00001m));
    }

    [Test]
    public async Task Handle_NullIngredientDto_ReturnsFailure()
    {
        // Arrange
        var command = new Create.Command { IngredientDto = null };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo("Failed to create ingredient."));
    }

    [Test]
    public async Task Handle_InvalidUser_ReturnsFailure()
    {
        // Arrange
        _userAccessor.Setup(x => x.GetUserId()).Returns((string)null);

        var command = new Create.Command
        {
            IngredientDto = new IngredientDto
            {
                Name = "Test Ingredient",
                Category = "Spice",
                MeasuredIn = "Weight"
            }
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Error, Is.EqualTo("Unable to retrieve the logged-in user."));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}