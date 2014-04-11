using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EM2014.Models.Mapping;

namespace EM2014.Models
{
    public partial class gbua_em2014Context : DbContext
    {
        static gbua_em2014Context()
        {
            Database.SetInitializer<gbua_em2014Context>(null);
        }

        public gbua_em2014Context()
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
