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

        // Categories
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void DeleteCategory(int id);
        int AddCategory(Category category);
        void SaveCategory(Category category);
        
        // ProductAttributes
        IEnumerable<ProductAttribute> GetProductAttributes();
        IEnumerable<ProductAttribute> GetProductAttributes(int categoryId);
        ProductAttribute GetProductAttribute(int id);
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
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        void DeleteProduct(int id);
        int AddProduct(Product product);
        void SaveProduct(Product product);

    }
}
