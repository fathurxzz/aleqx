using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Kiki.DataAccess.Entities;

namespace Kiki.DataAccess.EntityFramework.Mapping
{
    public class ContentMap : EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.MenuTitle)
                .HasMaxLength(200);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(1000);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(1000);

            this.Property(t => t.SeoText)
                .HasMaxLength(10000);

            this.Property(t => t.Text)
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("Content", "gbua_kiki");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.MenuTitle).HasColumnName("MenuTitle");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.SeoText).HasColumnName("SeoText");
            this.Property(t => t.Text).HasColumnName("Text");
        }
    }
}
