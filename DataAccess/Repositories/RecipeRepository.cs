using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _appDbContext;

        public RecipeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Recipe>?> GetRecipesAsync(int pageSize, int pageCount)
        {
            return await _appDbContext.Recipes
                .AsNoTracking()
                .OrderBy (x => x.Id)
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeDetailsBySlugAsync(string slug)
        {
            return await _appDbContext.Recipes
                .AsNoTracking()
                .Include(x => x.RecipesIngredients)
                  .ThenInclude(w => w.Ingredient)
                .Include(r => r.RecipesIngredients)
                  .ThenInclude(z => z.Unit)
                .Include(y => y.RecipeSteps)
                .FirstOrDefaultAsync(recipe => recipe.Slug.Equals(slug));
        }

        public async Task<int> GetRecipesCountAync()
        {
            return await _appDbContext.Recipes.CountAsync();
        }
    }
}
