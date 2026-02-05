namespace Business.DTOs
{
    public class RecipeIngredientDTO
    {
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Alternative { get; set; }
        public int CategoryId { get; set; }

        public string? UnitName { get; set; }
        public string? UnitAbbr { get; set; }
    }
}
