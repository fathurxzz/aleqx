using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EM2014.Models.Mapping
{
    public class ProductItemMap : EntityTypeConfiguration<ProductItem>
    {
        public ProductItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            this.Property(t => t.ImageSource)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ProductItem", "gbua_em2014");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductItems)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
