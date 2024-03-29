using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Shop.DatabaseModel.DataAccess.Models.Mapping
{
    public class ProductAttributeLangMap : EntityTypeConfiguration<ProductAttributeLang>
    {
        public ProductAttributeLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.UnitTitle)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductAttributeLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.UnitTitle).HasColumnName("UnitTitle");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.ProductAttributeId).HasColumnName("ProductAttributeId");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ProductAttributeLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.ProductAttribute)
                .WithMany(t => t.ProductAttributeLangs)
                .HasForeignKey(d => d.ProductAttributeId);

        }
    }
}
