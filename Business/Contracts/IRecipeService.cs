using Business.DTOs;

namespace Business.Contracts
{
    public interface IRecipeService
    {
        Task<RecipesPageDTO> GetRecipesAsync(int pageSize, int pageNumber);

        Task<RecipeDetailsDTO?> GetRecipeDetailsBySlugAsync(string slug);
    }
}
