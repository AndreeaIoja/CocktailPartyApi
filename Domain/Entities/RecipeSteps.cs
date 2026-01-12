namespace Domain.Entities
{
    public class RecipeSteps : BaseEntity
    {
        public int Id { get; set; }
        public int RecipeId {  get; set; }
        public int StepNumber { get; set; }
        public string? Description { get; set; }

        public virtual required Recipe Recipe { get; set; }



    }
}
