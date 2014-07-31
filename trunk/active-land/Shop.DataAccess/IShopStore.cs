using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        IDbSet<Product> Products { get; }
        IDbSet<ProductLang> ProductLangs { get; }

        int SaveChanges();
    }
}
