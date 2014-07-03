using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class ProductLangMap : EntityTypeConfiguration<ProductLang>
    {
        public ProductLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(1073741823);
            
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductLang", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.Title).HasColumnName("Title");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ProductLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductLangs)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
