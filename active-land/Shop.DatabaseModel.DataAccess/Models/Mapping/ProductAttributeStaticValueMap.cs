using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class ProductAttributeStaticValueMap : EntityTypeConfiguration<ProductAttributeStaticValue>
    {
        public ProductAttributeStaticValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductAttributeStaticValue", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductAttributeId).HasColumnName("ProductAttributeId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductAttributeStaticValues)
                .HasForeignKey(d => d.ProductId);
            this.HasRequired(t => t.ProductAttribute)
                .WithMany(t => t.ProductAttributeStaticValues)
                .HasForeignKey(d => d.ProductAttributeId);

        }
    }
}
