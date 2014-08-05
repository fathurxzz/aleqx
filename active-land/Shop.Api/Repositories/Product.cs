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
        public IEnumerable<Product> GetProducts()
        {
            var products = _store.Products.ToList();
            foreach (var product in products)
            {
                product.CurrentLang = LangId;
                product.Category.CurrentLang = LangId;
            }
            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _store.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new Exception(string.Format("Product with id={0} not found", id));
            }
            product.CurrentLang = LangId;

            foreach (var productAttributeValue in product.ProductAttributeValues)
            {
                productAttributeValue.CurrentLang = LangId;
            }

            foreach (var productAttributeStaticValue in product.ProductAttributeStaticValues)
            {
                productAttributeStaticValue.CurrentLang = LangId;
            }

            return product;
        }

        public void DeleteProduct(int id, Action<String> deleteImages)
        {
            var product = _store.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
            {
                throw new Exception(string.Format("Product with id={0} not found", id));
            }

            while (product.ProductLangs.Any())
            {
                var productLang = product.ProductLangs.First();
                _store.ProductLangs.Remove(productLang);
            }

            while (product.ProductAttributeValues.Any())
            {
                var pav = product.ProductAttributeValues.First();

                while (pav.ProductAttributeValueLangs.Any())
                {
                    var pavl = pav.ProductAttributeValueLangs.First();
                    pav.ProductAttributeValueLangs.Remove(pavl);
                }

                _store.ProductAttributeValues.Remove(pav);
            }

            while (product.ProductImages.Any())
            {
                var pi = product.ProductImages.First();
                deleteImages(pi.ImageSource);
                //var images = deleteImages;
                //images(pi.ImageSource);
                _store.ProductImages.Remove(pi);
            }

            _store.Products.Remove(product);
            _store.SaveChanges();
        }

        public int AddProduct(Product product)
        {
            if (_store.Products.Any(c => c.Name == product.Name))
            {
                throw new Exception(string.Format("Category {0} already exists", product.Name));
            }

            _store.Products.Add(product);

            CreateOrChangeEntityLanguage(product);

            _store.SaveChanges();
            return product.Id;
        }

        public void SaveProduct(Product product)
        {
            var cache = _store.Products.Single(c => c.Id == product.Id);
            //if (cache.Name != category.Name)
            //{
            //    if (_store.Categories.Any(c => c.Name == category.Name))
            //    {
            //        throw new Exception(string.Format("Category {0} already exists", category.Name));
            //    }
            //}


            //cache.Name = category.Name;
            //cache.SortOrder = category.SortOrder;
            //cache.Title = category.Title;
            //cache.SeoDescription = category.SeoDescription;
            //cache.SeoKeywords = category.SeoKeywords;
            //cache.SeoText = category.SeoText;

            CreateOrChangeEntityLanguage(cache);

            _store.SaveChanges();
        }

        private void CreateOrChangeEntityLanguage(Product cache)
        {
            var categoryLang = _store.ProductLangs.FirstOrDefault(r => r.ProductId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ProductLang
                {
                    ProductId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    Description = cache.Description,
                    SeoDescription = cache.SeoDescription,
                    SeoKeywords = cache.SeoKeywords,
                    SeoText = cache.SeoText,
                };
                _store.ProductLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.Description = cache.Description;
                categoryLang.SeoDescription = cache.SeoDescription;
                categoryLang.SeoKeywords = cache.SeoKeywords;
                categoryLang.SeoText = cache.SeoText;
            }

        }
    }
}
