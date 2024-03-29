using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class ProductAttributeValueMap : EntityTypeConfiguration<ProductAttributeValue>
    {
        public ProductAttributeValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductAttributeValue", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ProductAttributeId).HasColumnName("ProductAttributeId");
            this.Property(t => t.ProductAttributeValueTagId).HasColumnName("ProductAttributeValueTagId");

            // Relationships
            this.HasRequired(t => t.ProductAttribute)
                .WithMany(t => t.ProductAttributeValues)
                .HasForeignKey(d => d.ProductAttributeId);
            this.HasOptional(t => t.ProductAttributeValueTag)
                .WithMany(t => t.ProductAttributeValues)
                .HasForeignKey(d => d.ProductAttributeValueTagId);

        }
    }
}
