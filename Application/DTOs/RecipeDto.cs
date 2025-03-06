namespace Application.DTOs;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string RecipeCategory { get; set; }
    public int ServingsPerRecipe { get; set; }
    public ICollection<string> Instructions { get; set; } = new List<string>();
    
    // Collection of MeasurementDto
    public List<MeasurementDto> Measurements { get; set; }

    // Total nutrition and pricing for the recipe
    public double TotalCalories { get; set; }
    public double TotalCarbs { get; set; }
    public double TotalFat { get; set; }
    public double TotalProtein { get; set; }
    public decimal TotalPrice { get; set; }
    
    // Nutrition and Pricing Per Serving
    public double CaloriesPerServing { get; set; }
    public double CarbsPerServing { get; set; }
    public double FatPerServing { get; set; }
    public double ProteinPerServing { get; set; }
    public decimal PricePerServing { get; set; }
    
    public string AppUserId { get; set; }
}