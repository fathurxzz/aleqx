using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ContentLangMap : EntityTypeConfiguration<ContentLang>
    {
        public ContentLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(10000);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(10000);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(1000);

            this.Property(t => t.SeoText)
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("ContentLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.SeoText).HasColumnName("SeoText");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.ContentLangs)
                .HasForeignKey(d => d.ContentId);
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ContentLangs)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
