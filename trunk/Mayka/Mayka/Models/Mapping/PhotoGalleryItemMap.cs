using System.Data.Entity.ModelConfiguration;
using Mayka.Models.Entities;

namespace Mayka.Models.Mapping
{
    class PhotoGalleryItemMap : EntityTypeConfiguration<PhotoGalleryItem>
    {
        public PhotoGalleryItemMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("PhotoGalleryItem", "SiteContainer");

            this.HasRequired(t => t.Content).WithMany(t => t.PhotoGalleryItems).HasForeignKey(t => t.ContentId);
        }
    }
}
