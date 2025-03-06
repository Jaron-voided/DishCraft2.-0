using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Models;

public class Recipe
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [Required]
    public Categories.RecipeCategory RecipeCategory { get; set; }
 
    [Required]
    public int ServingsPerRecipe { get; set; }

    // Using JSON serialization built into EF Core
    [Required]
    public string[] Instructions { get; set; } = Array.Empty<string>();
    
    // Navigation Property
    public ICollection<Measurement>? Measurements { get; set; }
    
    [JsonIgnore]
    public AppUser? AppUser { get; set; } // Navigation property
    
    [JsonIgnore]
    public string? AppUserId { get; set; } // Foreign key
}