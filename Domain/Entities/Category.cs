namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual required ICollection<Ingredient> Ingredients { get; set; }
    }
}
