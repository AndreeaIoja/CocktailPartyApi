using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>()
                .HasOne(c => c.Category)
                .WithMany(s => s.Ingredients)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipesIngredients>()
                .HasOne(c => c.Unit)
                .WithMany(s => s.RecipesIngredients)
                .HasForeignKey(c => c.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipesIngredients>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipesIngredients>()
                .HasOne(st => st.Ingredient)
                .WithMany(s => s.RecipesIngredients)
                .HasForeignKey(st => st.IngredientId);

            modelBuilder.Entity<RecipesIngredients>()
                .HasOne(st => st.Recipe)
                .WithMany(t => t.RecipesIngredients)
                .HasForeignKey(st => st.RecipeId);

            modelBuilder.Entity<RecipeSteps>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeSteps)
                .HasForeignKey(rs => rs.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enforce unique step number per recipe
            modelBuilder.Entity<RecipeSteps>()
                .HasIndex(rs => new { rs.RecipeId, rs.StepNumber })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<RecipesIngredients> RecipesIngredients => Set<RecipesIngredients>();
        public DbSet<RecipeSteps> RecipeSteps => Set<RecipeSteps>();
    }
}
