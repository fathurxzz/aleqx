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
        public IEnumerable<ProductAttributeValueTag> GetProductAttributeValueTags()
        {
            var productAttributeValueTags = _store.ProductAttributeValueTags.ToList();
            foreach (var pavt in productAttributeValueTags)
            {
                pavt.CurrentLang = LangId;
            }
            return (productAttributeValueTags);
        }

        public ProductAttributeValueTag GetProductAttributeValueTag(int id)
        {
            var pavt = _store.ProductAttributeValueTags.SingleOrDefault(c => c.Id == id);
            if (pavt == null)
            {
                return null;
            }
            pavt.CurrentLang = LangId;
            return pavt;
        }

        public void DeleteProductAttributeValueTag(int id)
        {
            var pavt = _store.ProductAttributeValueTags.SingleOrDefault(c => c.Id == id);

            if (pavt == null)
            {
                throw new Exception(string.Format("ProductAttributeValueTag with id={0} doesn't found", id));
            }

            while (pavt.ProductAttributeValueTagLangs.Any())
            {
                var pavtLang = pavt.ProductAttributeValueTagLangs.First();
                _store.ProductAttributeValueTagLangs.Remove(pavtLang);
            }
            _store.ProductAttributeValueTags.Remove(pavt);
            _store.SaveChanges();

        }

        public int AddProductAttributeValueTag(ProductAttributeValueTag productAttributeValueTag)
        {
            _store.ProductAttributeValueTags.Add(productAttributeValueTag);
            CreateOrChangeEntityLanguage(productAttributeValueTag);
            _store.SaveChanges();
            return productAttributeValueTag.Id;
        }

        public void SaveProductAttributeValueTag(ProductAttributeValueTag productAttributeValueTag)
        {
            var cache = _store.ProductAttributeValueTags.Single(c => c.Id == productAttributeValueTag.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        private void CreateOrChangeEntityLanguage(ProductAttributeValueTag cache)
        {
            var categoryLang = _store.ProductAttributeValueTagLangs.FirstOrDefault(r => r.ProductAttributeValueTagId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductAttributeValueTagLang
                {
                    ProductAttributeValueTagId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                };
                _store.ProductAttributeValueTagLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
            }

        }
    }
}
