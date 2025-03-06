/*public class CreateDayPlanHandlerTests
{
    // Similar setup to above
    
    [Fact]
    public async Task Handle_CreateDayPlanWithRecipes_ReturnsSuccess()
    {
        // Create test recipe first
        var recipe = new Recipe
        {
            Id = Guid.NewGuid(),
            Name = "Test Recipe",
            RecipeCategory = Categories.RecipeCategory.Dinner,
            ServingsPerRecipe = 4,
            Instructions = new[] { "Step 1", "Step 2" },
            AppUserId = "test-user-id"
        };
        
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        
        // Arrange
        var dayPlanDto = new DayPlanDto
        {
            Name = "Monday Plan",
            DayPlanRecipes = new List<DayPlanRecipeDto>
            {
                new DayPlanRecipeDto
                {
                    RecipeId = recipe.Id,
                    Servings = 2
                }
            }
        };
        
        var command = new CreateDayPlan.Command { DayPlanDto = dayPlanDto };
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        
        // Verify day plan was created with correct recipe link
        var savedDayPlan = await _context.DayPlans
            .Include(dp => dp.DayPlanRecipes)
            .ThenInclude(dpr => dpr.Recipe)
            .FirstOrDefaultAsync(dp => dp.Name == "Monday Plan");
            
        Assert.NotNull(savedDayPlan);
        Assert.Single(savedDayPlan.DayPlanRecipes);
        Assert.Equal(recipe.Id, savedDayPlan.DayPlanRecipes.First().RecipeId);
        Assert.Equal(2, savedDayPlan.DayPlanRecipes.First().Servings);
    }
}*/