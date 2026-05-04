using Business.Contracts;
using Business.DTOs;
using CocktailParty.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailParty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IAuthService authService) : Controller
    {

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO?>> Register(UserRequestDTO request)
        {
            var user = await authService.RegisterAsync(request.Email, request.Password);
            if (user is null)
            {
                return BadRequest("User already exists");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO?>> Login(UserRequestDTO request)
        {
            var result = await authService.LoginAsync(request.Email, request.Password);
            if (result is null)
            {
                return BadRequest($"Failed to login {nameof(Login)}");
            }
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserResponseDTO?>> RefreshToken(RefreshTokenRequestDTO request)
        {
            var response = await authService.RefreshTokensAsync(request.UserId, request.RefreshToken);
            if (response is null || response.RefreshToken is null || response.Token is null)
            {
                return Unauthorized("You are unauthorized");
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("authorize")]
        public IActionResult AuthorizedEndpoint()
        {
            return Ok("You are authorized!");
        }
    }
}
