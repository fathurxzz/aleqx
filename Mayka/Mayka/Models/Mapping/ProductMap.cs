using System.Data.Entity.ModelConfiguration;
using Mayka.Models.Entities;

namespace Mayka.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("Product", "SiteContainer");

            this.HasRequired(t => t.Content).WithMany(t => t.Products).HasForeignKey(t => t.ContentId);
        }
    }
}