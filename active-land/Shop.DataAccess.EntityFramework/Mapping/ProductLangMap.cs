using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ProductLangMap : EntityTypeConfiguration<ProductLang>
    {
        public ProductLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoText)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("ProductLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.SeoText).HasColumnName("SeoText");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

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
