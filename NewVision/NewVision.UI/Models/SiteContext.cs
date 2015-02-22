using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NewVision.UI.Models.Mapping;

namespace NewVision.UI.Models
{
    public partial class SiteContext : DbContext
    {
        static SiteContext()
        {
            Database.SetInitializer<SiteContext>(null);
        }

        public SiteContext()
            : base("Name=SiteContext")
        {
        }

        public DbSet<EventAnnouncement> EventAnnouncements { get; set; }
        public DbSet<EventAnnouncementImage> EventAnnouncementImages { get; set; }
        public DbSet<MainBanner> MainBanners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EventAnnouncementMap());
            modelBuilder.Configurations.Add(new EventAnnouncementImageMap());
            modelBuilder.Configurations.Add(new MainBannerMap());
        }
    }
}
