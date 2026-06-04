using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
