using AutoMapper;
using Business.Contracts;
using Business.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Security.Cryptography;
using System.Text;

namespace Business.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper) : IAuthService
    {
        public async Task<UserResponseDTO?> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user is null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var response = await CreateTokenResponse(user);
            return response;
        }

        public async Task<UserResponseDTO?> RegisterAsync(string email, string password)
        {
            if(await userRepository.GetUserByEmailAsync(email) is not null)
            {
                return null;
            }

            var user = new User();
            var passwordHash = new PasswordHasher<User>().HashPassword(user, password);
            user.PasswordHash = passwordHash; 
            user.Email = email;

            await userRepository.AddUserAsync(user);

            return mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO?> RefreshTokensAsync(Guid userId, string refreshToken)
        {
            var user = await ValidateRefreshTokenAsync(userId, refreshToken);
            if (user is null)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        private async Task<UserResponseDTO> CreateTokenResponse(User user)
        {
            return new UserResponseDTO
            {
                Id= user.Id,
                Email = user.Email,
                Token = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken
                (issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            await userRepository.UpdateUserAsync(user, refreshToken);
            return refreshToken;
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

    }
}
