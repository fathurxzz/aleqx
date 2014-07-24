using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ProductAttributeValueLangMap : EntityTypeConfiguration<ProductAttributeValueLang>
    {
        public ProductAttributeValueLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductAttributeValueLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ProductAttributeValueId).HasColumnName("ProductAttributeValueId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ProductAttributeValueLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.ProductAttributeValue)
                .WithMany(t => t.ProductAttributeValueLangs)
                .HasForeignKey(d => d.ProductAttributeValueId);

        }
    }
}
