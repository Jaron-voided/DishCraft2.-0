using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.EntityHandlers.Ingredients;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public IngredientDto IngredientDto { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.IngredientDto).SetValidator(new IngredientValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Ingredient dto name: {request.IngredientDto.Name}");

            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Id == request.IngredientDto.Id, cancellationToken: cancellationToken);

            if (ingredient == null)
            {
                Console.WriteLine($"Ingredient with ID {request.IngredientDto.Id} not found.");
                return Result<Unit>.Failure("Ingredient not found");
            }

            Console.WriteLine($"Found ingredient: {ingredient.Name}, ID: {ingredient.Id}");

            // ✅ Map DTO properties to Ingredient (excluding MeasurementUnit)
            _mapper.Map(request.IngredientDto, ingredient);
            Console.WriteLine($"Mapped Ingredient: {ingredient.Id}, AppUserId: {ingredient.AppUserId}");
            
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to update ingredient.");
        }
    }
}