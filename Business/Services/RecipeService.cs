using AutoMapper;
using Business.Contracts;
using Business.DTOs;
using Business.Helpers;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Business.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper, IDistributedCache cache)
        {
            _repository = recipeRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<RecipesPageDTO> GetRecipesAsync(int pageSize, int pageNumber)
        {
            var cacheKey = $"recipes:page:{pageNumber}:size:{pageSize}";

            var cached = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<RecipesPageDTO>(cached) ?? new RecipesPageDTO
                {
                    Recipes = new List<RecipeDTO>(),
                    IsLastPage = true
                };
            }

            var recipes = await _repository.GetRecipesAsync(pageSize, pageNumber);
            var recipesCount = await _repository.GetRecipesCountAync();

            if (recipes == null || !recipes.Any())
            {
                return new RecipesPageDTO
                { 
                    Recipes = new List<RecipeDTO>(),
                    IsLastPage = true
                };
            }

            var pagedRecipes = new PagedList<Recipe>(recipes, pageNumber, pageSize, recipesCount);
            var recipesPageDto = _mapper.Map<RecipesPageDTO>(pagedRecipes);

            await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(recipesPageDto),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
            );

            return recipesPageDto;
        }

        public async Task<RecipeDetailsDTO?> GetRecipeDetailsBySlugAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }

            var cacheKey = $"recipeDetails: {slug}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (cached != null)
            {
                return JsonSerializer.Deserialize<RecipeDetailsDTO>(cached);
            }

            var recipeDetails = await _repository.GetRecipeDetailsBySlugAsync(slug);
            var recipeDetailsDto = recipeDetails == null ? null : _mapper.Map<RecipeDetailsDTO>(recipeDetails);

            await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(recipeDetailsDto),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
            );

            return recipeDetailsDto;

        }
    }
}
