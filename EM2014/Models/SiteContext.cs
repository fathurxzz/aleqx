using System.Data.Entity;
using EM2014.Models.Mapping;

namespace EM2014.Models
{
    public class SiteContext : DbContext
    {
        static SiteContext()
        {
            Database.SetInitializer<gbua_em2014Context>(null);
        }

         public SiteContext()
            : base("Name=gbua_em2014Context")
        {

        }

        public DbSet<Content> Contents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductItemMap());
        }
    }
}