using System.Data.Entity.ModelConfiguration;

namespace Mayka.Models.Entities.Mapping
{
    class PhotoGalleryItemMap : EntityTypeConfiguration<PhotoGalleryItem>
    {
        public PhotoGalleryItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ImageSource)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("PhotoGalleryItem", "gbua_mayka");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.ContentId).HasColumnName("ContentId");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.PhotoGalleryItems)
                .HasForeignKey(d => d.ContentId);
        }
    }
}
