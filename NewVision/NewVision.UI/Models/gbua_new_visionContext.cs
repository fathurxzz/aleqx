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

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorCategory> AuthorCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentImage> ContentImages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventAnnouncement> EventAnnouncements { get; set; }
        public DbSet<EventAnnouncementImage> EventAnnouncementImages { get; set; }
        public DbSet<MainBanner> MainBanners { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<PreviewContentImage> PreviewContentImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new ArticleImageMap());
            modelBuilder.Configurations.Add(new AuthorMap());
            modelBuilder.Configurations.Add(new AuthorCategoryMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new ContentImageMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new EventAnnouncementMap());
            modelBuilder.Configurations.Add(new EventAnnouncementImageMap());
            modelBuilder.Configurations.Add(new MainBannerMap());
            modelBuilder.Configurations.Add(new MediaMap());
            modelBuilder.Configurations.Add(new PreviewContentImageMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new TagMap());
        }
    }
}
