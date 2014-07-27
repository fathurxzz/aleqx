using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class ProductAttributeStaticValueLangMap : EntityTypeConfiguration<ProductAttributeStaticValueLang>
    {
        public ProductAttributeStaticValueLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductAttributeStaticValueLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ProductAttributeStaticValueId).HasColumnName("ProductAttributeStaticValueId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ProductAttributeStaticValueLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.ProductAttributeStaticValue)
                .WithMany(t => t.ProductAttributeStaticValueLangs)
                .HasForeignKey(d => d.ProductAttributeStaticValueId);

        }
    }
}
