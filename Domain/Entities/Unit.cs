namespace Domain.Entities
{
    public class Unit : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abbr { get; set; }

        public virtual ICollection<RecipesIngredients>? RecipesIngredients { get; set; }
    }
}
