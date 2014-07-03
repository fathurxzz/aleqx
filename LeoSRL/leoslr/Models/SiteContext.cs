using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Leo.Models.Mapping;

namespace Leo.Models
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
        public DbSet<ArticleItem> ArticleItems { get; set; }
        public DbSet<ArticleItemImage> ArticleItemImages { get; set; }
        public DbSet<ArticleItemLang> ArticleItemLangs { get; set; }
        public DbSet<ArticleLang> ArticleLangs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }
        public DbSet<CategoryLang> CategoryLangs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductLang> ProductLangs { get; set; }
        public DbSet<SpecialContent> SpecialContents { get; set; }
        public DbSet<SpecialContentLang> SpecialContentLangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new ArticleItemMap());
            modelBuilder.Configurations.Add(new ArticleItemImageMap());
            modelBuilder.Configurations.Add(new ArticleItemLangMap());
            modelBuilder.Configurations.Add(new ArticleLangMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CategoryImageMap());
            modelBuilder.Configurations.Add(new CategoryLangMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductImageMap());
            modelBuilder.Configurations.Add(new ProductLangMap());
            modelBuilder.Configurations.Add(new SpecialContentMap());
            modelBuilder.Configurations.Add(new SpecialContentLangMap());
        }
    }
}
