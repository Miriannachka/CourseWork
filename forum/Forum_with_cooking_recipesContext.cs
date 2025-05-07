using System.Data.Entity;


namespace forum
{
    public class Forum_with_cooking_recipesContext : DbContext
    {
        public Forum_with_cooking_recipesContext() : base("name=Entities")
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<RecipeRatings> RecipeRatings { get; set; }
    }

}