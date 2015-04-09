using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        public IEnumerable<ProductAttribute> GetProductAttributes()
        {
            return _store.ProductAttributes;
        }

        public IEnumerable<ProductAttribute> GetProductAttributes(string categoryName)
        {
            return _store.Categories.Single(c => c.Name == categoryName).ProductAttributes;
        }

        public IEnumerable<ProductAttribute> GetProductAttributes(int categoryId)
        {
            //Log.DebugFormat("GetProductAttributes started");
            var categories =_store.Categories.Include(c => c.ProductAttributes.Select(pa => pa.ProductAttributeValues));
            //Log.DebugFormat("GetProductAttributes categories_count:{0}", categories.Count());

            if (categories.Any())
            {
                //Log.DebugFormat("GetProductAttributes started");
                var category = categories.Single(x => x.Id == categoryId);

                foreach (var productAttribute in category.ProductAttributes)
                {
                    productAttribute.CurrentLang = LangId;

                    foreach (var value in productAttribute.ProductAttributeValues)
                    {
                        value.CurrentLang = LangId;
                    }
                }
                //Log.DebugFormat("GetProductAttributes finished");
                return category.ProductAttributes;
            }
            Log.Error("cant find category");
            //Log.DebugFormat("GetProductAttributes finished");
            return null;
        }

        public ProductAttribute GetProductAttribute(int id)
        {
            var productAttibute = _store.ProductAttributes.SingleOrDefault(c => c.Id == id);
            if (productAttibute == null)
            {
                throw new Exception(string.Format("ProductAttibute with id={0} not found", id));
            }
            productAttibute.CurrentLang = LangId;

            foreach (var pav in productAttibute.ProductAttributeValues)
            {
                pav.CurrentLang = LangId;

                if (pav.ProductAttributeValueTag != null)
                {
                    pav.ProductAttributeValueTag.CurrentLang = LangId;
                }
            }

            foreach (var pasv in productAttibute.ProductAttributeStaticValues)
            {
                pasv.CurrentLang = LangId;
            }


            return productAttibute;
        }

        public ProductAttribute GetProductAttribute(string externalId)
        {
            var productAttibute = _store.ProductAttributes.SingleOrDefault(c => c.ExternalId == externalId);
            if (productAttibute == null)
            {
                throw new Exception(string.Format("ProductAttibute with ExternalId={0} not found", externalId));
            }
            productAttibute.CurrentLang = LangId;

            foreach (var pav in productAttibute.ProductAttributeValues)
            {
                pav.CurrentLang = LangId;

                if (pav.ProductAttributeValueTag != null)
                {
                    pav.ProductAttributeValueTag.CurrentLang = LangId;
                }
            }

            foreach (var pasv in productAttibute.ProductAttributeStaticValues)
            {
                pasv.CurrentLang = LangId;
            }


            return productAttibute;
        }

        public void DeleteProductAttribute(int id)
        {
            var productAttribute = _store.ProductAttributes.SingleOrDefault(c => c.Id == id);

            if (productAttribute == null)
            {
                throw new Exception(string.Format("ProductAttribute with id={0} not found", id));
            }

            while (productAttribute.ProductAttributeLangs.Any())
            {
                var productAttributeLang = productAttribute.ProductAttributeLangs.First();
                _store.ProductAttributeLangs.Remove(productAttributeLang);
            }

            while (productAttribute.ProductAttributeValues.Any())
            {
                var pav = productAttribute.ProductAttributeValues.First();
                while (pav.ProductAttributeValueLangs.Any())
                {
                    var pavl = pav.ProductAttributeValueLangs.First();
                    _store.ProductAttributeValueLangs.Remove(pavl);
                }
                pav.Products.Clear();
                _store.ProductAttributeValues.Remove(pav);
            }

            while (productAttribute.ProductAttributeStaticValues.Any())
            {
                var pav = productAttribute.ProductAttributeStaticValues.First();
                while (pav.ProductAttributeStaticValueLangs.Any())
                {
                    var pavl = pav.ProductAttributeStaticValueLangs.First();
                    _store.ProductAttributeStaticValueLangs.Remove(pavl);
                }
                pav.Product = null;
                //pav.Products.Clear();
                _store.ProductAttributeStaticValues.Remove(pav);
            }

            productAttribute.Categories.Clear();

            _store.ProductAttributes.Remove(productAttribute);
            _store.SaveChanges();
        }

        public int AddProductAttribute(ProductAttribute productAttribute)
        {
            _store.ProductAttributes.Add(productAttribute);
            CreateOrChangeEntityLanguage(productAttribute);
            _store.SaveChanges();
            return productAttribute.Id;
        }

        public void SaveProductAttribute(ProductAttribute productAttribute)
        {
            var cache = _store.ProductAttributes.Single(c => c.Id == productAttribute.Id);

            //cache.UnitTitle = productAttribute.UnitTitle;
            //cache.SortOrder = productAttribute.SortOrder;
            //cache.Title = productAttribute.Title;

            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        private void CreateOrChangeEntityLanguage(ProductAttribute cache)
        {
            var categoryLang = _store.ProductAttributeLangs.FirstOrDefault(r => r.ProductAttributeId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeLang
                {
                    ProductAttributeId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    UnitTitle = cache.UnitTitle,
                };
                _store.ProductAttributeLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.UnitTitle = cache.UnitTitle;
            }

        }
    }
}
