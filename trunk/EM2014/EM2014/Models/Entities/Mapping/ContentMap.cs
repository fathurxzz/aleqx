using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EM2014.Models.Mapping
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
                .HasMaxLength(255);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoDescription)
                .HasMaxLength(1073741823);

            this.Property(t => t.SeoKeywords)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("Content", "gbua_em2014");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SeoDescription).HasColumnName("SeoDescription");
            this.Property(t => t.SeoKeywords).HasColumnName("SeoKeywords");
            this.Property(t => t.IsHomepage).HasColumnName("IsHomepage");
        }
    }
}
