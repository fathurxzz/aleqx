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
            this.Property(t => t.TitleEn)
                .HasMaxLength(500);
            this.Property(t => t.TitleUa)
                .HasMaxLength(500);

            this.Property(t => t.TitleDescription)
                .HasMaxLength(5000);
            this.Property(t => t.TitleDescriptionEn)
                .HasMaxLength(5000);
            this.Property(t => t.TitleDescriptionUa)
                .HasMaxLength(5000);

            this.Property(t => t.HighlightedText)
                .HasMaxLength(1000);
            this.Property(t => t.HighlightedTextEn)
                .HasMaxLength(1000);
            this.Property(t => t.HighlightedTextUa)
                .HasMaxLength(1000);

            this.Property(t => t.LocationAddress)
                .HasMaxLength(1000);
            this.Property(t => t.LocationAddressEn)
                .HasMaxLength(1000);
            this.Property(t => t.LocationAddressUa)
                .HasMaxLength(1000);

            this.Property(t => t.LocationTitle)
                .HasMaxLength(1000);
            this.Property(t => t.LocationTitleEn)
                .HasMaxLength(1000);
            this.Property(t => t.LocationTitleUa)
                .HasMaxLength(1000);

            this.Property(t => t.Description)
                .HasMaxLength(5000);
            this.Property(t => t.DescriptionEn)
                .HasMaxLength(5000);
            this.Property(t => t.DescriptionUa)
                .HasMaxLength(5000);

            this.Property(t => t.PreviewContentImageSrc)
                .HasMaxLength(1000);

            this.Property(t => t.Action)
                .HasMaxLength(1073741823);
            this.Property(t => t.ActionEn)
                .HasMaxLength(1073741823);
            this.Property(t => t.ActionUa)
                .HasMaxLength(1073741823);

            this.Property(t => t.Location)
                .HasMaxLength(1073741823);
            this.Property(t => t.LocationEn)
                .HasMaxLength(1073741823);
            this.Property(t => t.LocationUa)
                .HasMaxLength(1073741823);

            this.Property(t => t.ArtGroup)
                .HasMaxLength(1073741823);
            this.Property(t => t.ArtGroupEn)
                .HasMaxLength(1073741823);
            this.Property(t => t.ArtGroupUa)
                .HasMaxLength(1073741823);

            this.Property(t => t.Duration)
                .HasMaxLength(100);

            this.Property(t => t.IntervalQuantity)
                .HasMaxLength(100);

            this.Property(t => t.Price)
                .HasMaxLength(100);

            this.Property(t => t.PreviewContentVideoSrc)
                .HasMaxLength(1000);

            this.Property(t => t.LocationAddressMapUrl)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Event", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");
            this.Property(t => t.TitleDescription).HasColumnName("TitleDescription");
            this.Property(t => t.TitleDescriptionEn).HasColumnName("TitleDescriptionEn");
            this.Property(t => t.TitleDescriptionUa).HasColumnName("TitleDescriptionUa");
            this.Property(t => t.HighlightedText).HasColumnName("HighlightedText");
            this.Property(t => t.HighlightedTextEn).HasColumnName("HighlightedTextEn");
            this.Property(t => t.HighlightedTextUa).HasColumnName("HighlightedTextUa");
            this.Property(t => t.IsHighlighted).HasColumnName("IsHighlighted");
            this.Property(t => t.LocationAddress).HasColumnName("LocationAddress");
            this.Property(t => t.LocationAddressEn).HasColumnName("LocationAddressEn");
            this.Property(t => t.LocationAddressUa).HasColumnName("LocationAddressUa");
            this.Property(t => t.LocationTitle).HasColumnName("LocationTitle");
            this.Property(t => t.LocationTitleEn).HasColumnName("LocationTitleEn");
            this.Property(t => t.LocationTitleUa).HasColumnName("LocationTitleUa");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionEn).HasColumnName("DescriptionEn");
            this.Property(t => t.DescriptionUa).HasColumnName("DescriptionUa");
            this.Property(t => t.TicketOrderType).HasColumnName("TicketOrderType");
            this.Property(t => t.PreviewContentType).HasColumnName("PreviewContentType");
            this.Property(t => t.PreviewContentImageSrc).HasColumnName("PreviewContentImageSrc");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.ActionEn).HasColumnName("ActionEn");
            this.Property(t => t.ActionUa).HasColumnName("ActionUa");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.LocationEn).HasColumnName("LocationEn");
            this.Property(t => t.LocationUa).HasColumnName("LocationUa");
            this.Property(t => t.ArtGroup).HasColumnName("ArtGroup");
            this.Property(t => t.ArtGroupEn).HasColumnName("ArtGroupEn");
            this.Property(t => t.ArtGroupUa).HasColumnName("ArtGroupUa");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.IntervalQuantity).HasColumnName("IntervalQuantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.PreviewContentVideoSrc).HasColumnName("PreviewContentVideoSrc");
            this.Property(t => t.LocationAddressMapUrl).HasColumnName("LocationAddressMapUrl");
        }
    }
}
