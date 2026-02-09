namespace Domain.Entities
{
    public class Unit : BaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Abbr { get; set; }

        public required virtual ICollection<RecipesIngredients> RecipesIngredients { get; set; }
    }
}
