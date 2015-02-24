using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.UI.Models.Mapping
{
    public class PreviewContentImageMap : EntityTypeConfiguration<PreviewContentImage>
    {
        public PreviewContentImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("PreviewContentImage", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.EventId).HasColumnName("EventId");

            // Relationships
            this.HasRequired(t => t.Event)
                .WithMany(t => t.PreviewContentImages)
                .HasForeignKey(d => d.EventId);

        }
    }
}
