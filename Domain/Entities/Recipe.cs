namespace Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        public string? Note { get; set; }
        public string? Slug { get; set; }

        public virtual required ICollection<RecipesIngredients> RecipesIngredients { get; set; }
        public virtual required ICollection<RecipeSteps> RecipeSteps { get; set; }

    }
}
