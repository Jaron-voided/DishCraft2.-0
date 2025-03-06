namespace Application.DTOs;

public class MeasurementDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    
    public Guid RecipeId { get; set; }
    public RecipeDto Recipe { get; set; }

    public Guid IngredientId { get; set; }
    public IngredientDto Ingredient { get; set; }
    
    // Nutrition and pricing per measurement
    public double CaloriesPerAmount { get; set; }
    public double CarbsPerAmount { get; set; }
    public double FatPerAmount { get; set; }
    public double ProteinPerAmount { get; set; }
    public decimal PricePerAmount { get; set; }
}