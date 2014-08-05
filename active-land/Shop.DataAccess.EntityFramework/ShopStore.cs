using System.Data.Entity;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.EntityFramework
{
    public class ShopStore : IShopStore
    {
        readonly ShopContext _context = new ShopContext();


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

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
