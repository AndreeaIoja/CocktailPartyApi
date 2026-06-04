using Business.DTOs;

namespace Business.Contracts
{
    public interface IAuthService
    {
        Task<UserResponseDTO?> RegisterAsync(string email, string password);
        Task<(UserResponseDTO? response, string refreshToken)> LoginAsync(string email, string password);
        Task<(UserResponseDTO? response, string refreshToken)> RefreshTokensAsync(string refreshToken);
    }
}
