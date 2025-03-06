using Application.DTOs;
using FluentValidation;

namespace Application.EntityHandlers.Ingredients;

public class IngredientValidator : AbstractValidator<IngredientDto>
{
    public IngredientValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
        RuleFor(i => i.PricePerPackage).NotEmpty();
        //Add more later
    }
}