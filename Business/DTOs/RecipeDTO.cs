namespace Business.DTOs
{
    public record RecipeDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        public string? Note { get; set; }
        public string? Slug { get; set; }
    }
}
