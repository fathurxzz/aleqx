using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Repositories
{
    public interface IShopRepository : IRepository
    {
        int LangId { get; set; }

        // Languages

        IEnumerable<Language> GetLanguages();
            
        // Categories
        IEnumerable<Category> GetCategories(bool showInactive = false);
        Category GetCategory(int id);
        Category GetCategory(string categoryName);
        void DeleteCategory(int id, Action<string> deleteImages);
        int AddCategory(Category category);
        void SaveCategory(Category category);
        
        // ProductAttributes
        IEnumerable<ProductAttribute> GetProductAttributes();
        IEnumerable<ProductAttribute> GetProductAttributes(int categoryId);
        IEnumerable<ProductAttribute> GetProductAttributes(string categoryName);
        ProductAttribute GetProductAttribute(int id);
        ProductAttribute GetProductAttribute(string externalId);
        void DeleteProductAttribute(int id);
        int AddProductAttribute(ProductAttribute productAttribute);
        void SaveProductAttribute(ProductAttribute productAttribute);
        
        // ProductAttributeValues
        int AddProductAttributeValue(ProductAttributeValue productAttributeValue);
        void SaveProductAttributeValue(ProductAttributeValue productAttributeValue);
        ProductAttributeValue GetProductAttributeValue(int id);
        void DeleteProductAttributeValue(int id);

        // ProductAttributeStaticValues
        int AddProductAttributeStaticValue(ProductAttributeStaticValue productAttributeStaticValue);
        void SaveProductAttributeStaticValue(ProductAttributeStaticValue productAttributeStaticValue);
        ProductAttributeStaticValue GetProductAttributeStaticValue(int id);
        ProductAttributeStaticValue GetProductAttributeStaticValue(int productAttributeId, int productId);
        void DeleteProductAttributeStaticValue(int id);

        // ProductAttributeValueTags
        IEnumerable<ProductAttributeValueTag> GetProductAttributeValueTags();
        ProductAttributeValueTag GetProductAttributeValueTag(int id);
        void DeleteProductAttributeValueTag(int id);
        int AddProductAttributeValueTag(ProductAttributeValueTag productAttributeValueTag);
        void SaveProductAttributeValueTag(ProductAttributeValueTag productAttributeValueTag);

        // Products
        IQueryable<Product> GetAllProducts();
        IEnumerable<Product> GetActiveProducts();
        IQueryable<Product> GetProductsByCategory(string categoryName);
        IQueryable<Product> GetProductsByQueryString(string query);
        Product FindProduct(int id);
        Product GetProduct(int id);
        Product GetProduct(string name);
        Product GetProductByExternalId(string externalId);
        void DeleteProduct(int id, Action<String> deleteImages);
        int AddProduct(Product product);
        void SaveProduct(Product product);

        // ProductImages
        ProductImage GetProductImage(int id);
        void DeleteProductImage(int id, Action<String> deleteImage);
        void SaveProductImage(ProductImage productImage);


        // Articles
        IEnumerable<Article> GetActiveArticles();
        IEnumerable<Article> GetArticles(bool showOnlyActive=false);
        Article GetArticle(int id);
        Article GetArticle(string name);
        void DeleteArticle(int id, Action<string> deleteImages);
        int AddArticle(Article article);
        void SaveArticle(Article article);

        // ArticleItems
        ArticleItem GetArticleItem(int id);
        void DeleteArticleItem(int id, Action<string> deleteImages);
        void SaveArticleItem(ArticleItem articleItem);
        int AddArticleItem(ArticleItem articleItem);
        
        // ArticleItemImages
        ArticleItemImage GetArticleItemImage(int id);
        void DeleteArticleItemImage(int id, Action<string> deleteImages);
        
        // Content
        IEnumerable<Content> GetContents();
        Content GetContent(int id);
        Content GetContent(string name);
        Content GetContent();
        Content GetCatalogueContent();
        void DeleteContent(int id, Action<string> deleteImages);
        void SaveContent(Content content);
        int AddContent(Content content);

        // ContentItems
        ContentItem GetContentItem(int id);
        void DeleteContntItem(int id, Action<string> deleteImages);
        void SaveContentItem(ContentItem contentItem);
        int AddContentItem(ContentItem contentItem);

        // ArticleItemImages
        ContentItemImage GetContentItemImage(int id);
        void DeleteContentItemImage(int id, Action<string> deleteImages);

        // Order
        int AddOrder(Order order);
        IEnumerable<Order> GetOrders();
        Order GetOrder(int id);
        void DeleteOrder(int id);
        void SaveOrder(Order order);

        // QuickAdvice
        int AddQuickAdvice(QuickAdvice quickAdvice);
        IEnumerable<QuickAdvice> GetQuickAdvices(bool showOnlyActive = false);
        QuickAdvice GetQuickAdvice(int id);
        void DeleteQuickAdvice(int id);
        void SaveQuickAdvice(QuickAdvice quickAdvice);


        // ShopSettings
        void SaveShopSettings(ShopSetting shopSettings);
        ShopSetting GetShopSetting(string key);
        ShopSetting GetShopSetting(int id);
        IEnumerable<ShopSetting> GetShopSettings();

        // ProductStocks
        ProductStock GetProductStock(int id);
        void DeleteProductStock(int id);
        void SaveProductStock(ProductStock productStock);

        // MainPageBanner
        int AddMainPageBanner(MainPageBanner mainPageBanner);
        void SaveMainPageBanner(MainPageBanner mainPageBanner);
        MainPageBanner GetMainPageBanner(int id);
        IEnumerable<MainPageBanner> GetMainPageBanners();
        IEnumerable<MainPageBanner> GetSiteBanners();
        void DeleteMainPageBanner(int id, Action<string> deleteImages);


    }
}
