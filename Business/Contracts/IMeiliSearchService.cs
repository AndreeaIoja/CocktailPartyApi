using Business.DTOs;

namespace Business.Contracts
{
    public interface IMeiliSearchService
    {
        Task IndexRecipesAsync();
        Meilisearch.Index GetIndex(string indexName);
        Task<IEnumerable<RecipeDTO>> SearchRecipesAsync(string query);
    }
}
