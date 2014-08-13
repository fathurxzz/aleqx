using System.Data.Entity;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework
{
    public class ShopStore : IShopStore
    {
        readonly ShopContext _context = new ShopContext();


        public IDbSet<Language> Languages
        {
            get { return _context.Languages; }
        }

        public IDbSet<Category> Categories
        {
            get { return _context.Categories; }
        }

        public IDbSet<CategoryLang> CategoryLangs
        {
            get { return _context.CategoryLangs; }
        }

        public IDbSet<ProductAttribute> ProductAttributes
        {
            get { return _context.ProductAttributes; }
        }

        public IDbSet<ProductAttributeLang> ProductAttributeLangs
        {
            get { return _context.ProductAttributeLangs; }
        }

        public IDbSet<ProductAttributeValue> ProductAttributeValues
        {
            get { return _context.ProductAttributeValues; }
        }

        public IDbSet<ProductAttributeValueLang> ProductAttributeValueLangs
        {
            get { return _context.ProductAttributeValueLangs; }
        }

        public IDbSet<ProductAttributeStaticValue> ProductAttributeStaticValues
        {
            get { return _context.ProductAttributeStaticValues; }
        }

        public IDbSet<ProductAttributeStaticValueLang> ProductAttributeStaticValueLangs
        {
            get { return _context.ProductAttributeStaticValueLangs; }
        }

        public IDbSet<ProductAttributeValueTag> ProductAttributeValueTags
        {
            get { return _context.ProductAttributeValueTags; }
        }

        public IDbSet<ProductAttributeValueTagLang> ProductAttributeValueTagLangs
        {
            get { return _context.ProductAttributeValueTagLangs; }
        }

        public IDbSet<Product> Products
        {
            get { return _context.Products; }
        }

        public IDbSet<ProductLang> ProductLangs
        {
            get { return _context.ProductLangs; }
        }

        public IDbSet<ProductImage> ProductImages
        {
            get { return _context.ProductImages; }
        }

        public IDbSet<Article> Articles
        {
            get { return _context.Articles; }
        }
        public IDbSet<ArticleLang> ArticleLangs
        {
            get { return _context.ArticleLangs; }
        }
        public IDbSet<ArticleItem> ArticleItems
        {
            get { return _context.ArticleItems; }
        }
        public IDbSet<ArticleItemLang> ArticleItemLangs
        {
            get { return _context.ArticleItemLangs; }
        }
        public IDbSet<ArticleItemImage> ArticleItemImages
        {
            get { return _context.ArticleItemImages; }
        }

        public IDbSet<Content> Contents
        {
            get { return _context.Contents; }
        }
        public IDbSet<ContentLang> ContentLangs
        {
            get { return _context.ContentLangs; }
        }

        public IDbSet<ContentItem> ContentItems
        {
            get { return _context.ContentItems; }
        }
        public IDbSet<ContentItemLang> ContentItemLangs
        {
            get { return _context.ContentItemLangs; }
        }
        public IDbSet<ContentItemImage> ContentItemImages
        {
            get { return _context.ContentItemImages; }
        }

        public IDbSet<Order> Orders
        {
            get { return _context.Orders; }
        }

        public IDbSet<OrderItem> OrderItems
        {
            get { return _context.OrderItems; }
        }

        public IDbSet<QuickAdvice> QuickAdvices
        {
            get { return _context.QuickAdvices; }
        }

        public IDbSet<QuickAdviceLang> QuickAdviceLangs
        {
            get { return _context.QuickAdviceLangs; }
        }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
