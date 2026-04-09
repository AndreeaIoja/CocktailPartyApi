namespace Business.DTOs
{
    public class RecipesPageDTO
    {
        public List<RecipeDTO>? Recipes { get; set; }
        public bool IsLastPage { get; set; } 
        public int TotalCount { get; set; }
    }
}
