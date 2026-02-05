using Business.DTOs;

namespace Business.Contracts
{
    public interface IRecipeService
    {
        Task<List<RecipeDTO>?> GetRecipesAsync(int pageSize, int pageCount);

        Task<RecipeDetailsDTO?> GetRecipeDetailsByIdAsync(int id);
    }
}
