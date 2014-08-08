using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ContentItemLangMap : EntityTypeConfiguration<ContentItemLang>
    {
        public ContentItemLangMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("ContentItemLang", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.ContentItemId).HasColumnName("ContentItemId");
            this.Property(t => t.LanguageId).HasColumnName("LanguageId");

            // Relationships
            this.HasRequired(t => t.ContentItem)
                .WithMany(t => t.ContentItemLangs)
                .HasForeignKey(d => d.ContentItemId);
            this.HasRequired(t => t.Language)
                .WithMany(t => t.ContentItemLangs)
                .HasForeignKey(d => d.LanguageId);

        }
    }
}
