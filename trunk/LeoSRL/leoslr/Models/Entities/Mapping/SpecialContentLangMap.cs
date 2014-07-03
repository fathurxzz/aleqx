using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class SpecialContentLangMap : EntityTypeConfiguration<SpecialContentLang>
    {
        public SpecialContentLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(1073741823);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("SpecialContentLang", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SpecialContentId).HasColumnName("SpecialContentId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.Language)
                .WithMany(t => t.SpecialContentLangs)
                .HasForeignKey(d => d.LanguageId);
            this.HasRequired(t => t.SpecialContent)
                .WithMany(t => t.SpecialContentLangs)
                .HasForeignKey(d => d.SpecialContentId);

        }
    }
}
