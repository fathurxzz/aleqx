using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        public int AddProductAttributeStaticValue(ProductAttributeStaticValue productAttributeStaticValue)
        {
            _store.ProductAttributeStaticValues.Add(productAttributeStaticValue);
            CreateOrChangeEntityLanguage(productAttributeStaticValue);
            _store.SaveChanges();
            return productAttributeStaticValue.Id;
        }

        public void SaveProductAttributeStaticValue(ProductAttributeStaticValue productAttributeStaticValue)
        {
            var cache = _store.ProductAttributeStaticValues.Single(c => c.Id == productAttributeStaticValue.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public ProductAttributeStaticValue GetProductAttributeStaticValue(int productAttributeId, int productId)
        {
            var productAttibuteStaticValue = _store.ProductAttributeStaticValues.SingleOrDefault(c => c.ProductAttributeId == productAttributeId && c.ProductId == productId);
            if (productAttibuteStaticValue != null)
            {
                productAttibuteStaticValue.CurrentLang = LangId;
                return productAttibuteStaticValue;
            }
            return null;
        }

        public ProductAttributeStaticValue GetProductAttributeStaticValue(int id)
        {
            var productAttibuteStaticValue = _store.ProductAttributeStaticValues.SingleOrDefault(c => c.Id == id);
            if (productAttibuteStaticValue == null)
            {
                throw new Exception(string.Format("ProductAttibuteStaticValue with id={0} not found", id));
            }
            productAttibuteStaticValue.CurrentLang = LangId;
            return productAttibuteStaticValue;
        }

        public void DeleteProductAttributeStaticValue(int id)
        {
            var productAttributeStaticValue = _store.ProductAttributeStaticValues.SingleOrDefault(c => c.Id == id);
            if (productAttributeStaticValue == null)
            {
                throw new Exception(string.Format("ProductAttributeStaticValue with id={0} not found", id));
            }

            while (productAttributeStaticValue.ProductAttributeStaticValueLangs.Any())
            {
                var productAttributeStaticValueLang = productAttributeStaticValue.ProductAttributeStaticValueLangs.First();
                _store.ProductAttributeStaticValueLangs.Remove(productAttributeStaticValueLang);
            }
            _store.ProductAttributeStaticValues.Remove(productAttributeStaticValue);
            _store.SaveChanges();
        }

        private void CreateOrChangeEntityLanguage(ProductAttributeStaticValue cache)
        {
            var categoryLang = _store.ProductAttributeStaticValueLangs.FirstOrDefault(r => r.ProductAttributeStaticValueId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeStaticValueLang
                {
                    ProductAttributeStaticValueId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                };
                _store.ProductAttributeStaticValueLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
            }

        }
    }
}
