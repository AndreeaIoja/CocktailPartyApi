using AutoMapper;
using Business.Contracts;
using Business.DTOs;
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
        public async Task<List<RecipeDTO>?> GetRecipesAsync(int pageSize, int pageCount)
        {
            var cacheKey = $"recipes:page:{pageCount}:size:{pageSize}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (cached != null)
            {
                return JsonSerializer.Deserialize<List<RecipeDTO>>(cached);
            }

            var recipes = await _repository.GetRecipesAsync(pageSize, pageCount);

            await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(recipes),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
            );

            return recipes == null ? null : _mapper.Map<List<RecipeDTO>>(recipes);
        }
    }
}
