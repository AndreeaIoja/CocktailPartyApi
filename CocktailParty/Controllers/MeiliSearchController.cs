using Business.Contracts;
using Business.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CocktailParty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeiliSearchController : ControllerBase
    {
        private readonly IMeiliSearchService _meiliSearchService;
        public MeiliSearchController(IMeiliSearchService meiliSearchService)
        {
            _meiliSearchService = meiliSearchService;   
        }

        [HttpPost("reindex")]
        public async Task<IActionResult> Reindex()
        {
            await _meiliSearchService.IndexRecipesAsync();
            return Ok("Indexare completă");
        }

        [HttpGet("searchRecipes")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> SearchRecipes([FromQuery] string query)
        {
            var results = await _meiliSearchService.SearchRecipesAsync(query);
            return Ok(results);
        }
    }
}
