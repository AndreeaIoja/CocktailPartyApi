namespace CocktailParty.DTOs
{
    public class RefreshTokenRequestDTO
    {
        public required Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
