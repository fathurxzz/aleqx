using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class PostItemMap : EntityTypeConfiguration<PostItem>
    {
        public PostItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("PostItem", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.PostId).HasColumnName("PostId");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.PostItems)
                .HasForeignKey(d => d.PostId);

        }
    }
}
