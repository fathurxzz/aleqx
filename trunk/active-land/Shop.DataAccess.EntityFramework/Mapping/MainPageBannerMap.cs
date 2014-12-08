using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework.Mapping
{
    public class MainPageBannerMap : EntityTypeConfiguration<MainPageBanner>
    {
        public MainPageBannerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.ImageSource)
                .HasMaxLength(200);

            this.Property(t => t.Url)
                .HasMaxLength(500);

            this.ToTable("MainPageBanner", "gbua_active_dev");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ImageSource).HasColumnName("ImageSource");
            this.Property(t => t.IsSiteBanner).HasColumnName("IsSiteBanner");
            this.Property(t => t.Url).HasColumnName("Url");
        }
    }
}
