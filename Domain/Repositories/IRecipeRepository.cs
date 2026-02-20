using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>?> GetRecipesAsync(int pageSize, int pageCount);
        Task<Recipe?> GetRecipeDetailsBySlugAsync(string slug);
        Task<int> GetRecipesCountAync();
        Task<List<Recipe>?> GetAllRecipesAsync();

    }
}
