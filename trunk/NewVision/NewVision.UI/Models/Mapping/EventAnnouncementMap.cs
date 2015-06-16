using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class EventAnnouncementMap : EntityTypeConfiguration<EventAnnouncement>
    {
        public EventAnnouncementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(500);
            this.Property(t => t.TitleEn)
                .IsRequired()
                .HasMaxLength(500);
            this.Property(t => t.TitleUa)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(10000);
            this.Property(t => t.TextEn)
                .IsRequired()
                .HasMaxLength(10000);
            this.Property(t => t.TextUa)
                .IsRequired()
                .HasMaxLength(10000);

            // Table & Column Mappings
            this.ToTable("EventAnnouncement", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.TextEn).HasColumnName("TextEn");
            this.Property(t => t.TextUa).HasColumnName("TextUa");
        }
    }
}
