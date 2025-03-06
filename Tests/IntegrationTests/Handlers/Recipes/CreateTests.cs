/*public class CreateRecipeHandlerTests
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly Mock<IUserAccessor> _userAccessor;
    private readonly CreateRecipe.Handler _handler;
    
    public CreateRecipeHandlerTests()
    {
        // Setup similar to the ingredients test
        // ...
        
        // Add a test ingredient
        var ingredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = "Test Ingredient",
            IngredientCategory = Categories.IngredientCategory.Spice,
            MeasuredIn = MeasurementUnits.MeasuredIn.Weight,
            WeightUnit = MeasurementUnits.WeightUnit.Grams,
            PricePerPackage = 5.00m,
            MeasurementsPerPackage = 500,
            AppUserId = "test-user-id"
        };
        
        _context.Ingredients.Add(ingredient);
        _context.SaveChanges();
    }
    
    [Fact]
    public async Task Handle_CreateRecipeWithMeasurements_ReturnsSuccess()
    {
        // Get the test ingredient
        var ingredient = await _context.Ingredients.FirstAsync();
        
        // Arrange
        var recipeDto = new RecipeDto
        {
            Name = "Test Recipe",
            RecipeCategory = "Dinner",
            ServingsPerRecipe = 4,
            Instructions = new[] { "Step 1", "Step 2" },
            Measurements = new List<MeasurementDto>
            {
                new MeasurementDto
                {
                    IngredientId = ingredient.Id,
                    Amount = 100 // 100 grams
                }
            }
        };
        
        var command = new CreateRecipe.Command { RecipeDto = recipeDto };
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        
        // Verify recipe was created
        var savedRecipe = await _context.Recipes
            .Include(r => r.Measurements)
            .ThenInclude(m => m.Ingredient)
            .FirstOrDefaultAsync(r => r.Name == "Test Recipe");
            
        Assert.NotNull(savedRecipe);
        Assert.Single(savedRecipe.Measurements);
        Assert.Equal(ingredient.Id, savedRecipe.Measurements.First().IngredientId);
        Assert.Equal(100, savedRecipe.Measurements.First().Amount);
    }
}*/