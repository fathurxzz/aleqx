using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Leo.Models.Mapping
{
    public class SpecialContentMap : EntityTypeConfiguration<SpecialContent>
    {
        public SpecialContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PageImageSource)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ContentImageSource)
                .IsRequired()
                .HasMaxLength(255);

            // Ignored
            this.Ignore(t => t.Title);
            this.Ignore(t => t.Text);
            this.Ignore(t => t.IsCorrectLang);
            this.Ignore(t => t.CurrentLang);

            // Table & Column Mappings
            this.ToTable("SpecialContent", "gbua_Leo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PageImageSource).HasColumnName("PageImageSource");
            this.Property(t => t.ContentImageSource).HasColumnName("ContentImageSource");

            this.Property(t => t.IsFirstCategory).HasColumnName("IsFirstCategory");
            this.Property(t => t.IsSecondCategory).HasColumnName("IsSecondCategory");
        }
    }
}
