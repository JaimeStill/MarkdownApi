using MarkdownApi.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MarkdownApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }
        
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Wiki> Wikis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
