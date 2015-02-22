using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class EventAnnouncementImageMap : EntityTypeConfiguration<EventAnnouncementImage>
    {
        public EventAnnouncementImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EventAnnouncementImage", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.EventAnnouncementId).HasColumnName("EventAnnouncementId");

            // Relationships
            this.HasRequired(t => t.EventAnnouncement)
                .WithMany(t => t.EventAnnouncementImages)
                .HasForeignKey(d => d.EventAnnouncementId);

        }
    }
}
