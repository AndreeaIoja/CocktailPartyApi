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
        public async Task<ActionResult<RecipesPageDTO>> GetRecipes([FromQuery] PageRequestDTO pageRequest)
        {
            var recipes = await _recipeService.GetRecipesAsync(pageRequest.PageSize, pageRequest.PageCount);
            return Ok(recipes);
        }

        [HttpGet("details")]
        public async Task<ActionResult<RecipeDetailsDTO>> GetRecipeDetails(string slug)
        {
            var recipeDetails = await _recipeService.GetRecipeDetailsBySlugAsync(slug);

            if (recipeDetails is null)
            {
                return NotFound();
            }

            return Ok(recipeDetails);
        }

    }
}
