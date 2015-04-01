using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FashionIntention.UI.Models.Mapping;

namespace FashionIntention.UI.Models
{
    public partial class SiteContext : DbContext
    {
        static SiteContext()
        {
            Database.SetInitializer<SiteContext>(null);
        }

        public SiteContext()
            : base("Name=gbua_fashintContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleItem> ArticleItems { get; set; }
        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<MainBanner> MainBanners { get; set; }
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostItem> PostItems { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new ArticleItemMap());
            modelBuilder.Configurations.Add(new ContentItemMap());
            modelBuilder.Configurations.Add(new MainBannerMap());
            modelBuilder.Configurations.Add(new MediaItemMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new PostItemMap());
            modelBuilder.Configurations.Add(new SubscriberMap());
            modelBuilder.Configurations.Add(new TagMap());
        }
    }
}
