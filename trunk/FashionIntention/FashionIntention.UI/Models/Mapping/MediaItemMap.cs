using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class MediaItemMap : EntityTypeConfiguration<MediaItem>
    {
        public MediaItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.VideoSrc)
                .HasMaxLength(500);

            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("MediaItem", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.VideoSrc).HasColumnName("VideoSrc");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Text).HasColumnName("Text");
        }
    }
}
