namespace Business.DTOs
{
    public record RecipeDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ImagePath { get; set; }
        public required string Note { get; set; }
        public required string Slug { get; set; }
    }
}
