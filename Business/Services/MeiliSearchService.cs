using AutoMapper;
using Business.Contracts;
using Business.DTOs;
using Domain.Repositories;
using Meilisearch;

namespace Business.Services
{
    public class MeiliSearchService : IMeiliSearchService
    {
        private readonly MeilisearchClient _client;
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;


        public MeiliSearchService(IRecipeRepository recipeRepository, MeilisearchClient meilisearchClient, IMapper mapper)
        {
            _client = meilisearchClient;
            _repository = recipeRepository;
            _mapper = mapper;
        }


        public async Task IndexRecipesAsync()
        {
            var recipes = await _repository.GetAllRecipesAsync();
            var recipesDto = _mapper.Map<List<RecipeDTO>>(recipes);

            var indexName = "recipes";
            var indexes = await _client.GetAllIndexesAsync();
            if (!indexes.Results.Any(i => i.Uid == indexName))
            {
                await _client.CreateIndexAsync(indexName, "id");
            }

            var index = _client.Index(indexName);

            await index.AddDocumentsAsync(recipesDto);

            var settings = await index.GetSettingsAsync();
            if (!settings.SearchableAttributes.Any())
            {
                await index.UpdateSearchableAttributesAsync(new[] { "Title", "Slug" });
            }

        }

        public Meilisearch.Index GetIndex(string indexName)
        {
            return _client.Index(indexName);
        }

        public async Task<IEnumerable<RecipeDTO>> SearchRecipesAsync(string query)
        {
            var index = _client.Index("recipes");
            var results = await index.SearchAsync<RecipeDTO>(query);
            return results.Hits;
        }
    }
}
