namespace Business.DTOs
{
    public class RecipeIngredientDTO
    {
        public required string Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Alternative { get; set; }
        public int CategoryId { get; set; }

        public required string UnitName { get; set; }
        public required string UnitAbbr { get; set; }
    }
}
