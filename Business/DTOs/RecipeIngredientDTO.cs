namespace Business.DTOs
{
    public class RecipeIngredientDTO
    {
        public required double Quantity { get; set; }
        public string? Note { get; set; }

        public required string Name { get; set; }
        public string? ImagePath { get; set; }
        public string? Alternative { get; set; }
        public required int CategoryId { get; set; }

        public required string UnitName { get; set; }
        public required string UnitAbbr { get; set; }
    }
}
