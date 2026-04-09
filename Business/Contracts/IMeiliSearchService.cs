using Business.DTOs;

namespace Business.Contracts
{
    public interface IMeiliSearchService
    {
        Task IndexRecipesAsync();
        Meilisearch.Index GetIndex(string indexName);
        Task<RecipesPageDTO> SearchRecipesAsync(string query, int pageSize, int pageNumber);
    }
}
