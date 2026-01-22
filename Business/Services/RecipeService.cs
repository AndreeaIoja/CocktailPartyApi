using AutoMapper;
using Business.Contracts;
using Business.DTOs;
using Domain.Repositories;

namespace Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _repository = recipeRepository;
            _mapper = mapper;

        }
        public async Task<List<RecipeDTO>?> GetRecipesAsync(int pageSize, int pageCount)
        {
            var recipes = await _repository.GetRecipesAsync(pageSize, pageCount);

            return recipes == null ? null : _mapper.Map<List<RecipeDTO>>(recipes);
        }
    }
}
