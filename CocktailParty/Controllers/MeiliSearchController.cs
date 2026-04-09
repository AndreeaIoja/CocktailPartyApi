using Business.Contracts;
using CocktailParty.DTOs;
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
        public async Task<IActionResult> SearchRecipes([FromQuery] string? query, [FromQuery] PageRequestDTO pageRequest)
        {
            var results = await _meiliSearchService.SearchRecipesAsync(query, pageRequest.PageSize, pageRequest.pageNumber);
            return Ok(results);
        }
    }
}
