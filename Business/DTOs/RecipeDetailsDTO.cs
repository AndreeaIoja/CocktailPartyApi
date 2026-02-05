namespace Business.DTOs
{
    public class RecipeDetailsDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        public string? Note { get; set; }
        public string? Slug { get; set; }

        public List<RecipeStepDTO>? RecipeSteps { get; set; }

        public List<RecipeIngredientDTO>? RecipeIngredients { get; set; }

    }
}
