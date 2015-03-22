using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class ContentMap : EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.Name)
                .HasMaxLength(500);

            this.Property(t => t.MenuTitle)
                .HasMaxLength(500);

            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("Content", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.MenuTitle).HasColumnName("MenuTitle");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
        }
    }
}
