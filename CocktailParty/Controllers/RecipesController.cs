using Business.Contracts;
using Business.DTOs;
using CocktailParty.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CocktailParty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
           _recipeService = recipeService; 
        }

        [HttpGet(Name = "GetRecipes")]
        public async Task<ActionResult<List<RecipeDTO>>> GetRecipes([FromQuery] PageRequestDTO pageRequest)
        {
            var recipes = await _recipeService.GetRecipesAsync(pageRequest.PageSize, pageRequest.PageCount);
            return Ok(recipes);
        }
    }
}
