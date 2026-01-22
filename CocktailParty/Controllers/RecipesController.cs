using Business.Contracts;
using CocktailParty.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CocktailParty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
           _recipeService = recipeService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipesAsync([FromQuery] PageRequestDTO pageRequest)
        {
            var recipes = await _recipeService.GetRecipesAsync(pageRequest.PageSize, pageRequest.PageCount);
            return Ok(recipes);
        }
    }
}
