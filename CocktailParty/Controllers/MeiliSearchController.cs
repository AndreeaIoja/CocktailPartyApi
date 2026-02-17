using Business.Contracts;
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

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _meiliSearchService.SearchAsync(query);
            return Ok(results);
        }
    }
}
