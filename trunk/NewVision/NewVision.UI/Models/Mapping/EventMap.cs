using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(500);

            this.Property(t => t.TitleDescription)
                .HasMaxLength(10000);

            this.Property(t => t.HighlightedText)
                .HasMaxLength(1000);

            this.Property(t => t.LocationAddress)
                .HasMaxLength(1000);

            this.Property(t => t.LocationTitle)
                .HasMaxLength(1000);

            this.Property(t => t.Description)
                .HasMaxLength(10000);

            this.Property(t => t.PreviewContentImageSrc)
                .HasMaxLength(1000);
            
            this.Property(t => t.PreviewContentVideoSrc)
                .HasMaxLength(1000);

            this.Property(t => t.Action)
                .HasMaxLength(1073741823);

            this.Property(t => t.Location)
                .HasMaxLength(1073741823);

            this.Property(t => t.ArtGroup)
                .HasMaxLength(1073741823);

            this.Property(t => t.Duration)
                .HasMaxLength(100);

            this.Property(t => t.IntervalQuantity)
                .HasMaxLength(100);

            this.Property(t => t.Price)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Event", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleDescription).HasColumnName("TitleDescription");
            this.Property(t => t.HighlightedText).HasColumnName("HighlightedText");
            this.Property(t => t.IsHighlighted).HasColumnName("IsHighlighted");
            this.Property(t => t.LocationAddress).HasColumnName("LocationAddress");
            this.Property(t => t.LocationTitle).HasColumnName("LocationTitle");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.TicketOrderType).HasColumnName("TicketOrderType");
            this.Property(t => t.PreviewContentType).HasColumnName("PreviewContentType");
            this.Property(t => t.PreviewContentImageSrc).HasColumnName("PreviewContentImageSrc");
            this.Property(t => t.PreviewContentVideoSrc).HasColumnName("PreviewContentVideoSrc");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.ArtGroup).HasColumnName("ArtGroup");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.IntervalQuantity).HasColumnName("IntervalQuantity");
            this.Property(t => t.Price).HasColumnName("Price");
        }
    }
}
