using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
           await _appDbContext.Users.AddAsync(user);
           await _appDbContext.SaveChangesAsync();

           return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> UpdateUserAsync(User user, string? refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _appDbContext.SaveChangesAsync();
            return user;
        }
    }
}
