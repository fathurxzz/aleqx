using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class ProductImageMap : EntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProductImage", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductImages)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
