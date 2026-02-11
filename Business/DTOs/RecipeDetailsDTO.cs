namespace Business.DTOs
{
    public class RecipeDetailsDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string ImagePath { get; set; }
        public required string Note { get; set; }
        public required string Slug { get; set; }

        public required List<RecipeStepDTO> RecipeSteps { get; set; }

        public required List<RecipeIngredientDTO> RecipeIngredients { get; set; }

    }
}
