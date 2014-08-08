using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class ContentItemImageMap : EntityTypeConfiguration<ContentItemImage>
    {
        public ContentItemImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ContentItemImage", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContentItemId).HasColumnName("ContentItemId");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");

            // Relationships
            this.HasRequired(t => t.ContentItem)
                .WithMany(t => t.ContentItemImages)
                .HasForeignKey(d => d.ContentItemId);

        }
    }
}
