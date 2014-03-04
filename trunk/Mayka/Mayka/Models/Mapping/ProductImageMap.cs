using System.Data.Entity.ModelConfiguration;
using Mayka.Models.Entities;

namespace Mayka.Models.Mapping
{
    public class ProductImageMap : EntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("ProductImage", "SiteContainer");

            this.HasRequired(t => t.Product).WithMany(t => t.ProductImages).HasForeignKey(t => t.ProductId);
        }
    }
}