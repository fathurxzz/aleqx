using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ProductAttributeValueTagLangMap : EntityTypeConfiguration<ProductAttributeValueTagLang>
    {
        public ProductAttributeValueTagLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductAttributeValueTagLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ProductAttributeValueTagId).HasColumnName("ProductAttributeValueTagId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.Title).HasColumnName("Title");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ProductAttributeValueTagLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.ProductAttributeValueTag)
                .WithMany(t => t.ProductAttributeValueTagLangs)
                .HasForeignKey(d => d.ProductAttributeValueTagId);

        }
    }
}
