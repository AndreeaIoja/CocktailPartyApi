namespace Domain.Entities
{
    public class RecipesIngredients : BaseEntity
    {
        public int RecipeId { get; set; }
        public virtual required Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public virtual required Ingredient Ingredient { get; set; }

        public int UnitId { get; set; }
        public double Quantity { get; set; }
        public string? Note { get; set; }

        public virtual required Unit Unit { get; set; }
    }
}
