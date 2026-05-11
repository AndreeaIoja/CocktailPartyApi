using Business.Contracts;
using Business.DTOs;
using CocktailParty.DTOs;
using Domain.Entities;
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
            var (userResponse, refreshToken) = await authService.LoginAsync(request.Email, request.Password);
            if ( userResponse is null || refreshToken is null )
            {
                return Unauthorized("Invalid credentials");
            }

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/api/identity/refresh-token"
            });

            return Ok(userResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserResponseDTO?>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token missing");
            }

            var (userResponse, newRefreshToken) = await authService.RefreshTokensAsync(refreshToken);
            if (userResponse is null || newRefreshToken is null || userResponse.Token is null)
            {
                return Unauthorized("You are unauthorized");
            }

            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/api/identity/refresh-token"
            });

            return Ok(userResponse);
        }

        [Authorize]
        [HttpGet("authorize")]
        public IActionResult AuthorizedEndpoint()
        {
            return Ok("You are authorized!");
        }
    }
}
