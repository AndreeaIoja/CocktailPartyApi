using System.ComponentModel.DataAnnotations;

namespace CocktailParty.DTOs
{
    public class UserRequestDTO
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }

    }
}
