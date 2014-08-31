using System.Data.Entity;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.EntityFramework.Mapping;


namespace Kiki.DataAccess.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SiteContext : DbContext
    {
        static SiteContext()
        {
            Database.SetInitializer<SiteContext>(null);
        }

        public SiteContext()
            : base("Name=gbua_kikiContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<SiteImage> SiteImages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new GalleryImageMap());
            modelBuilder.Configurations.Add(new ReasonMap());
            modelBuilder.Configurations.Add(new SaleMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new ServiceItemMap());
            modelBuilder.Configurations.Add(new SiteImageMap());
            modelBuilder.Configurations.Add(new SubscriberMap());
        }
    }
}
