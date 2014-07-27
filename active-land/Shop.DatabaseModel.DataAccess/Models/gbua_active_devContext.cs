using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Shop.DatabaseModel.DataAccess.Models.Mapping;

namespace Shop.DatabaseModel.DataAccess.Models
{
    public partial class gbua_active_devContext : DbContext
    {
        static gbua_active_devContext()
        {
            Database.SetInitializer<gbua_active_devContext>(null);
        }

        public gbua_active_devContext()
            : base("Name=gbua_active_devContext")
        {
        }

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
        public DbSet<ProductLang> ProductLangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
            modelBuilder.Configurations.Add(new ProductLangMap());
        }
    }
}
