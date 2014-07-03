using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
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

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("CategoryLang", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.Text).HasColumnName("Text");

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
