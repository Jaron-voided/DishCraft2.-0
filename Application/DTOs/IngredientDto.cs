namespace Application.DTOs;

public class IngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    
    public string MeasuredIn { get; set; }
    public string? WeightUnit { get; set; }
    public string? VolumeUnit { get; set; }
    
    public decimal PricePerPackage { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerMeasurement { get; set; }
    
    public double Calories { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    
    public string AppUserId { get; set; }
}