using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NewVision.DataModel.Models.Mapping;

namespace NewVision.DataModel.Models
{
    public partial class gbua_new_visionContext : DbContext
    {
        static gbua_new_visionContext()
        {
            Database.SetInitializer<gbua_new_visionContext>(null);
        }

        public gbua_new_visionContext()
            : base("Name=gbua_new_visionContext")
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
        public DbSet<Medium> Media { get; set; }
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
            modelBuilder.Configurations.Add(new MediumMap());
            modelBuilder.Configurations.Add(new PreviewContentImageMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new TagMap());
        }
    }
}
