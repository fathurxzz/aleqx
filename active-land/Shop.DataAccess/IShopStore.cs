using System.Data.Entity;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess
{
    public interface IShopStore
    {
        IDbSet<Category> Categories { get; }
        IDbSet<CategoryLang> CategoryLangs { get; }

        IDbSet<ProductAttribute> ProductAttributes { get; }
        IDbSet<ProductAttributeLang> ProductAttributeLangs { get; }

        IDbSet<ProductAttributeValue> ProductAttributeValues { get; }
        IDbSet<ProductAttributeValueLang> ProductAttributeValueLangs { get; }

        IDbSet<ProductAttributeStaticValue> ProductAttributeStaticValues { get; }
        IDbSet<ProductAttributeStaticValueLang> ProductAttributeStaticValueLangs { get; }

        IDbSet<ProductAttributeValueTag> ProductAttributeValueTags { get; }
        IDbSet<ProductAttributeValueTagLang> ProductAttributeValueTagLangs { get; }

        IDbSet<Product> Products { get; }
        IDbSet<ProductLang> ProductLangs { get; }
        
        IDbSet<ProductImage> ProductImages { get; }

        IDbSet<Article> Articles { get; }
        IDbSet<ArticleLang> ArticleLangs { get; }

        IDbSet<ArticleItem> ArticleItems { get; }
        IDbSet<ArticleItemLang> ArticleItemLangs { get; }

        IDbSet<ArticleItemImage> ArticleItemImages { get; }

        IDbSet<Content> Contents { get; }
        IDbSet<ContentLang> ContentLangs { get; }

        IDbSet<ContentItem> ContentItems { get; }
        IDbSet<ContentItemLang> ContentItemLangs { get; }

        IDbSet<ContentItemImage> ContentItemImages { get; }

        int SaveChanges();
    }
}
