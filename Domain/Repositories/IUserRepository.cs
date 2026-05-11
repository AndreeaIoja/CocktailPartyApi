using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user, string? refreshToken);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}
