using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.Description)
                .HasMaxLength(1000);

            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Post", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Published).HasColumnName("Published");

            // Relationships
            this.HasMany(t => t.Tags)
                .WithMany(t => t.Posts)
                .Map(m =>
                    {
                        m.ToTable("PostTag", "gbua_fashint");
                        m.MapLeftKey("Posts_Id");
                        m.MapRightKey("Tags_Id");
                    });


        }
    }
}
