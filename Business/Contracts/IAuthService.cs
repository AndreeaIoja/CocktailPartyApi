using Business.DTOs;

namespace Business.Contracts
{
    public interface IAuthService
    {
        Task<UserResponseDTO?> RegisterAsync(string email, string password);
        Task<UserResponseDTO?> LoginAsync(string email, string password);
        Task<UserResponseDTO?> RefreshTokensAsync(Guid userId, string refreshToken);
    }
}
