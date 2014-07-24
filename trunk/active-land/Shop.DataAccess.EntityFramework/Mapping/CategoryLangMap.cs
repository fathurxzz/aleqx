using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;


namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class CategoryLangMap : EntityTypeConfiguration<CategoryLang>
    {
        public CategoryLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoText)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("CategoryLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.SeoText).HasColumnName("SeoText");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.CategoryLangs)
                .HasForeignKey(d => d.CategoryId);
            this.HasRequired(t => t.Language)
                .WithMany(t => t.CategoryLangs)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
