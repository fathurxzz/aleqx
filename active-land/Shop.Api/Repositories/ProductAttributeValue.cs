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
        public int AddProductAttributeValue(ProductAttributeValue productAttributeValue)
        {
            _store.ProductAttributeValues.Add(productAttributeValue);
            CreateOrChangeEntityLanguage(productAttributeValue);
            _store.SaveChanges();
            return productAttributeValue.Id;
        }

        public void SaveProductAttributeValue(ProductAttributeValue productAttributeValue)
        {
            var cache = _store.ProductAttributeValues.Single(c => c.Id == productAttributeValue.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public ProductAttributeValue GetProductAttributeValue(int id)
        {
            var productAttibuteValue = _store.ProductAttributeValues.SingleOrDefault(c => c.Id == id);
            if (productAttibuteValue == null)
            {
                throw new Exception(string.Format("ProductAttibuteValue with id={0} not found", id));
            }
            productAttibuteValue.CurrentLang = LangId;

            if (productAttibuteValue.ProductAttributeValueTag != null)
            {
                productAttibuteValue.ProductAttributeValueTag.CurrentLang = LangId;
            }

            return productAttibuteValue;
        }

        public void DeleteProductAttributeValue(int id)
        {
            var productAttributeValue = _store.ProductAttributeValues.SingleOrDefault(c => c.Id == id);
            if (productAttributeValue == null)
            {
                throw new Exception(string.Format("ProductAttributeValue with id={0} not found", id));
            }

            while (productAttributeValue.ProductAttributeValueLangs.Any())
            {
                var productAttributeValueLang = productAttributeValue.ProductAttributeValueLangs.First();
                _store.ProductAttributeValueLangs.Remove(productAttributeValueLang);
            }
            _store.ProductAttributeValues.Remove(productAttributeValue);
            _store.SaveChanges();
        }

        private void CreateOrChangeEntityLanguage(ProductAttributeValue cache)
        {
            var categoryLang = _store.ProductAttributeValueLangs.FirstOrDefault(r => r.ProductAttributeValueId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeValueLang
                {
                    ProductAttributeValueId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                };
                _store.ProductAttributeValueLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
            }

        }
    }
}
