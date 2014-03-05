using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mayka.Models.Entities;

namespace Mayka.Models
{
    public partial class SiteContext:DbContext
    {
        public SiteContext()
            : base("name=gbua_mayka")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new PhotoGalleryItemMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductImageMap());
        }


        public DbSet<Content> Content { get; set; }
        public DbSet<PhotoGalleryItem> PhotoGalleryItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }

    }
}