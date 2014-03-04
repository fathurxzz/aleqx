using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mayka.Models.Entities;

namespace Mayka.Models
{
    public class SiteContext:DbContext
    {
        public SiteContext()
            : base("name=ModelContainer")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }


        public DbSet<Content> Content { get; set; }
        public DbSet<PhotoGalleryItem> PhotoGalleryItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }

    }
}