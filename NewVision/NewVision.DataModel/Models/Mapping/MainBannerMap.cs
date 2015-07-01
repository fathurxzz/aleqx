using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NewVision.DataModel.Models.Mapping
{
    public class MainBannerMap : EntityTypeConfiguration<MainBanner>
    {
        public MainBannerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ImageSrc)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.TitleEn)
                .HasMaxLength(500);

            this.Property(t => t.TitleUa)
                .HasMaxLength(500);

            this.Property(t => t.DescriptionEn)
                .HasMaxLength(2000);

            this.Property(t => t.DescriptionUa)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("MainBanner", "gbua_new_vision");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.TitleEn).HasColumnName("TitleEn");
            this.Property(t => t.TitleUa).HasColumnName("TitleUa");
            this.Property(t => t.DescriptionEn).HasColumnName("DescriptionEn");
            this.Property(t => t.DescriptionUa).HasColumnName("DescriptionUa");
        }
    }
}
