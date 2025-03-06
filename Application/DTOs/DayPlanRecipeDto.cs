namespace Application.DTOs;

public class DayPlanRecipeDto
{
    public Guid Id { get; set; }
    
    public Guid DayPlanId { get; set; }
    public DayPlanDto DayPlan { get; set; }
    
    public Guid RecipeId { get; set; }
    public RecipeDto Recipe { get; set; }
    
    public int Servings { get; set; }
    
    // Nutrition and Pricing
    public double Calories { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    public decimal Price { get; set; }
}