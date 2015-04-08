using System.Data.Entity;
using log4net;
using Shop.DataAccess.Entities;
using Shop.DataAccess.EntityFramework.Mapping;

namespace Shop.DataAccess.EntityFramework
{

   

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ShopContext:DbContext
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ShopContext));

        static ShopContext()
        {
            Database.SetInitializer<ShopContext>(null);
        }

        public ShopContext()
            : base("Name=gbua_active_dev")
        {
            //Database.Log = Log.Debug;
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleItem> ArticleItems { get; set; }
        public DbSet<ArticleItemImage> ArticleItemImages { get; set; }
        public DbSet<ArticleItemLang> ArticleItemLangs { get; set; }
        public DbSet<ArticleLang> ArticleLangs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLang> CategoryLangs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeLang> ProductAttributeLangs { get; set; }
        public DbSet<ProductAttributeStaticValue> ProductAttributeStaticValues { get; set; }
        public DbSet<ProductAttributeStaticValueLang> ProductAttributeStaticValueLangs { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductAttributeValueLang> ProductAttributeValueLangs { get; set; }
        public DbSet<ProductAttributeValueTag> ProductAttributeValueTags { get; set; }
        public DbSet<ProductAttributeValueTagLang> ProductAttributeValueTagLangs { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<ProductLang> ProductLangs { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<ContentItemImage> ContentItemImages { get; set; }
        public DbSet<ContentItemLang> ContentItemLangs { get; set; }
        public DbSet<ContentLang> ContentLangs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<QuickAdvice> QuickAdvices { get; set; }
        public DbSet<QuickAdviceLang> QuickAdviceLangs { get; set; }
        public DbSet<ShopSetting> ShopSettings  { get; set; }
        public DbSet<MainPageBanner> MainPageBanners  { get; set; }
        public DbSet<SiteProperty> SiteProperties { get; set; }


        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new ArticleItemMap());
            modelBuilder.Configurations.Add(new ArticleItemImageMap());
            modelBuilder.Configurations.Add(new ArticleItemLangMap());
            modelBuilder.Configurations.Add(new ArticleLangMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CategoryLangMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductAttributeMap());
            modelBuilder.Configurations.Add(new ProductAttributeLangMap());
            modelBuilder.Configurations.Add(new ProductAttributeStaticValueMap());
            modelBuilder.Configurations.Add(new ProductAttributeStaticValueLangMap());
            modelBuilder.Configurations.Add(new ProductAttributeValueMap());
            modelBuilder.Configurations.Add(new ProductAttributeValueLangMap());
            modelBuilder.Configurations.Add(new ProductAttributeValueTagMap());
            modelBuilder.Configurations.Add(new ProductAttributeValueTagLangMap());
            modelBuilder.Configurations.Add(new ProductImageMap());
            modelBuilder.Configurations.Add(new ProductStockMap());
            modelBuilder.Configurations.Add(new ProductLangMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new ContentItemMap());
            modelBuilder.Configurations.Add(new ContentItemImageMap());
            modelBuilder.Configurations.Add(new ContentItemLangMap());
            modelBuilder.Configurations.Add(new ContentLangMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderItemMap());
            modelBuilder.Configurations.Add(new QuickAdviceMap());
            modelBuilder.Configurations.Add(new QuickAdviceLangMap());
            modelBuilder.Configurations.Add(new ShopSettingMap());
            modelBuilder.Configurations.Add(new MainPageBannerMap());
            modelBuilder.Configurations.Add(new SitePropertyMap());
        }
    }
}
