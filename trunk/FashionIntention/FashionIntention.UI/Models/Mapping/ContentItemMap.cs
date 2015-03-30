using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class ContentItemMap : EntityTypeConfiguration<ContentItem>
    {
        public ContentItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("ContentItem", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Text).HasColumnName("Text");
        }
    }
}
