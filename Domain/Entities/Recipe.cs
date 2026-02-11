using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string ImagePath { get; set; }
        public required string Note { get; set; }
        public required string Slug { get; set; }

        public virtual required ICollection<RecipesIngredients> RecipesIngredients { get; set; }
        public virtual required ICollection<RecipeSteps> RecipeSteps { get; set; }

    }
}
