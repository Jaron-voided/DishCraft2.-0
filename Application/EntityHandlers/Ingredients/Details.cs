using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.EntityHandlers.Ingredients;

public class Details
{
    public class Query : IRequest<Result<IngredientDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<IngredientDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IngredientDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var ingredient = await _context.Ingredients
                .ProjectTo<IngredientDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            return Result<IngredientDto>.Success(ingredient);
        }
    }
}