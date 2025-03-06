using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Models;

#nullable enable

public class Ingredient
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public Categories.IngredientCategory Category { get; set; }

    
    // Measurement Type Enum
    [Required]
    public MeasurementUnits.MeasuredIn MeasuredIn { get; set; }
    
    // Specific Unit based on MeasuredIn type
    public MeasurementUnits.WeightUnit? WeightUnit { get; set; }
    public MeasurementUnits.VolumeUnit? VolumeUnit { get; set; }
    
    [Required]
    public decimal PricePerPackage { get; set; }
    [Required]
    public int Quantity { get; set; }
    
    public double Calories { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public double Protein { get; set; }
    
    [JsonIgnore]
    public AppUser? AppUser { get; set; } // Navigation property
    
    [JsonIgnore]
    public string? AppUserId { get; set; } // Foreign key
}