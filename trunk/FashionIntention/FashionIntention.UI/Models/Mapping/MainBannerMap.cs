using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FashionIntention.UI.Models.Mapping
{
    public class MainBannerMap : EntityTypeConfiguration<MainBanner>
    {
        public MainBannerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Url)
                .HasMaxLength(500);

            this.Property(t => t.ImageSrc)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("MainBanner", "gbua_fashint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.ImageSrc).HasColumnName("ImageSrc");
        }
    }
}
