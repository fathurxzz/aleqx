using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public partial class ShopRepository : IShopRepository
    {
        //public IEnumerable<Product> GetProducts()
        //{
        //    var products = _store.Products.ToList();
        //    foreach (var product in products)
        //    {
        //        product.CurrentLang = LangId;
        //        product.Category.CurrentLang = LangId;

        //        if (product.ProductImages.Any())
        //        {
        //            var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
        //            product.ImageSource = pi.ImageSource;
        //        }
        //    }
        //    return products;
        //}

        public IEnumerable<Product> GetAllProducts()
        {
            return _store.Products;
        }
        public IEnumerable<Product> GetActiveProducts()
        {
            var products = _store.Products.Where(p => p.IsActive).ToList();
            foreach (var product in products)
            {
                product.CurrentLang = LangId;
                product.Category.CurrentLang = LangId;

                if (product.ProductImages.Any())
                {
                    var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                    product.ImageSource = pi.ImageSource;
                }
            }
            return products;
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            return _store.Products.Where(p => p.Category.Name == categoryName);
        }

        public Product GetProduct(int id)
        {
            var product = _store.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new ObjectNotFoundException(string.Format("Product with id={0} not found", id));
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
            if (product.ProductImages.Any())
            {
                var pi = product.ProductImages.FirstOrDefault(c => c.IsDefault) ?? product.ProductImages.First();
                product.ImageSource = pi.ImageSource;
            }

            return product;
        }

        public Product GetProduct(string name)
        {
            var product = _store.Products.SingleOrDefault(p => p.Name == name);
            if (product == null)
            {
                throw new ObjectNotFoundException(string.Format("Product with name={0} not found", name));
            }
            product.CurrentLang = LangId;

            foreach (var productAttributeValue in product.ProductAttributeValues)
            {
                productAttributeValue.CurrentLang = LangId;

                if (productAttributeValue.ProductAttributeValueTag != null)
                {
                    productAttributeValue.ProductAttributeValueTag.CurrentLang = LangId;
                }
            }

            foreach (var productAttributeStaticValue in product.ProductAttributeStaticValues)
            {
                productAttributeStaticValue.CurrentLang = LangId;
            }

            foreach (var productAttribute in product.Category.ProductAttributes)
            {
                productAttribute.CurrentLang = LangId;
            }

            return product;
        }

        public void DeleteProduct(int id, Action<String> deleteImages)
        {
            var product = _store.Products.SingleOrDefault(c => c.Id == id);

            if (product == null)
            {
                throw new ObjectNotFoundException(string.Format("Product with id={0} not found", id));
            }

            product.Category = null;

            while (product.ProductLangs.Any())
            {
                var productLang = product.ProductLangs.First();
                _store.ProductLangs.Remove(productLang);
            }
            product.ProductLangs = null;
            //_store.SaveChanges();

            //while (product.ProductAttributeValues.Any())
            //{
            //    var pav = product.ProductAttributeValues.First();

            //    while (pav.ProductAttributeValueLangs.Any())
            //    {
            //        var pavl = pav.ProductAttributeValueLangs.First();
            //        //_store.ProductAttributeValueLangs.Remove(pavl);
            //        pav.ProductAttributeValueLangs.Remove(pavl);
            //    }
            //    pav.ProductAttributeValueLangs = null;
            //    product.ProductAttributeValues.Remove(pav);
            //}
            product.ProductAttributeValues = null;
            //_store.SaveChanges();

            //while (product.ProductAttributeStaticValues.Any())
            //{
            //    var pav = product.ProductAttributeStaticValues.First();

            //    while (pav.ProductAttributeStaticValueLangs.Any())
            //    {
            //        var pavl = pav.ProductAttributeStaticValueLangs.First();
            //        //_store.ProductAttributeStaticValueLangs.Remove(pavl);
            //        pav.ProductAttributeStaticValueLangs.Remove(pavl);
            //    }
            //    pav.ProductAttributeStaticValueLangs = null;
            //    product.ProductAttributeStaticValues.Remove(pav);
            //}
            product.ProductAttributeStaticValues = null;
            //_store.SaveChanges();

            while (product.ProductImages.Any())
            {
                var pi = product.ProductImages.First();
                deleteImages(pi.ImageSource);
                _store.ProductImages.Remove(pi);
            }
            product.ProductImages = null;
            //_store.SaveChanges();

            _store.Products.Remove(product);
            _store.SaveChanges();
        }

        public int AddProduct(Product product)
        {
            if (_store.Products.Any(c => c.Name == product.Name))
            {
                throw new ObjectNotFoundException(string.Format("Product {0} already exists", product.Name));
            }

            _store.Products.Add(product);

            CreateOrChangeEntityLanguage(product);

            string searchCriteria = product.ProductLangs.Aggregate("", (current, productLang) => current + (productLang.Title + " "));

            product.SearchCriteria = searchCriteria + " " + product.Name;
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
            string searchCriteria = product.ProductLangs.Aggregate("", (current, productLang) => current + (productLang.Title + " "));
            product.SearchCriteria = searchCriteria + " " + product.Name;
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
