using Application.Utilities;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class IngredientService
{
    private readonly IngredientConversions _conversions;

    public IngredientService(IngredientConversions conversions)
    {
        _conversions = conversions;
    }

    public decimal ConvertToBaseUnit(Ingredient ingredient)
    {

        return ingredient.MeasuredIn switch
        {
            MeasurementUnits.MeasuredIn.Weight => ingredient.WeightUnit switch
            {
                MeasurementUnits.WeightUnit.Pounds => _conversions.ConvertPoundsToGrams(ingredient.Quantity),
                MeasurementUnits.WeightUnit.Ounces => _conversions.ConvertOuncesToGrams(ingredient.Quantity),
                MeasurementUnits.WeightUnit.Kilograms => _conversions.ConvertKilosToGrams(ingredient.Quantity),
                _ => throw new InvalidOperationException("Unsupported weight unit.")
            },
            MeasurementUnits.MeasuredIn.Volume => ingredient.VolumeUnit switch
            {
                MeasurementUnits.VolumeUnit.FluidOunces => _conversions.ConvertFluidOuncesToMilliliters(ingredient.Quantity),
                MeasurementUnits.VolumeUnit.Cups => _conversions.ConvertCupsToMilliliters(ingredient.Quantity),
                MeasurementUnits.VolumeUnit.Liters => _conversions.ConvertLitersToMilliliters(ingredient.Quantity),
                MeasurementUnits.VolumeUnit.Gallons => _conversions.ConvertGallonsToMilliliters(ingredient.Quantity),
                _ => throw new InvalidOperationException("Unsupported volume unit.")
            },
            MeasurementUnits.MeasuredIn.Each => ingredient.Quantity,
            _ => throw new InvalidOperationException("Unsupported measurement type.")
        };
    }
}