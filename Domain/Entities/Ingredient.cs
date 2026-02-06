namespace Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Alternative {  get; set; }
        public int CategoryId { get; set; }

        public virtual required Category Category { get; set; }
        public virtual required ICollection<RecipesIngredients> RecipesIngredients { get; set; }

    }
}
