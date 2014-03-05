using System.Data.Entity.ModelConfiguration;

namespace Mayka.Models.Entities
{
    public class ContentMap:EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.MenuTitle)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Text)
                .HasMaxLength(65535);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(65535);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(65535);

            // Table & Column Mappings
            this.ToTable("Content", "gbua_mayka");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.MenuTitle).HasColumnName("MenuTitle");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.ContentType).HasColumnName("ContentType");

        }
    }
}