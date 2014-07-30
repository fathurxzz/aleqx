using System;
using System.Collections.Generic;
using System.Linq;
using Shop.DataAccess;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly IShopStore _store;

        public ShopRepository(IShopStore store)
        {
            _store = store;
        }

        public int LangId { get; set; }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _store.Categories.ToList();
            foreach (var category in categories)
            {
                category.CurrentLang = LangId;
            }
            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = _store.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new Exception(string.Format("Category with id={0} not found", id));
            }
            category.CurrentLang = LangId;
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _store.Categories.SingleOrDefault(c => c.Id == id);

            if (category==null)
            {
                throw new Exception(string.Format("Category with id={0} doesn't found", id));
            }

            while (category.CategoryLangs.Any())
            {
                var categoryLang = category.CategoryLangs.First();
                _store.CategoryLangs.Remove(categoryLang);
            }

            while (category.ProductAttributes.Any())
            {
                var pa = category.ProductAttributes.First();

                while (pa.ProductAttributeLangs.Any())
                {
                    var pal = pa.ProductAttributeLangs.First();
                    pa.ProductAttributeLangs.Remove(pal);
                }

                _store.ProductAttributes.Remove(pa);
            }

            _store.Categories.Remove(category);
            _store.SaveChanges();

        }

        public int AddCategory(Category category)
        {
            if (_store.Categories.Any(c => c.Name == category.Name))
            {
                throw new Exception(string.Format("Category {0} already exists", category.Name));
            }

            _store.Categories.Add(category);

            CreateOrChangeEntityLanguage(category);

            _store.SaveChanges();
            return category.Id;
        }

        public void SaveCategory(Category category)
        {
            var cache = _store.Categories.Single(c => c.Id == category.Id);
            //if (cache.Name != category.Name)
            //{
            //    if (_store.Categories.Any(c => c.Name == category.Name))
            //    {
            //        throw new Exception(string.Format("Category {0} already exists", category.Name));
            //    }
            //}
            cache.Name = category.Name;
            cache.SortOrder = category.SortOrder;
            cache.Title = category.Title;
            cache.SeoDescription = category.SeoDescription;
            cache.SeoKeywords = category.SeoKeywords;
            cache.SeoText = category.SeoText;
            
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public IEnumerable<ProductAttribute> GetProductAttributes()
        {
            var productAttributes = _store.ProductAttributes.ToList();
            foreach (var productAttribute in productAttributes)
            {
                productAttribute.CurrentLang = LangId;
            }
            return productAttributes;
        }

        public IEnumerable<ProductAttribute> GetProductAttributes(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ProductAttribute GetProductAttribute(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductAttribute(int id)
        {
            throw new NotImplementedException();
        }

        public int AddProductAttribute(ProductAttribute productAttribute)
        {
            throw new NotImplementedException();
        }

        public void SaveProductAttribute(ProductAttribute productAttribute)
        {
            throw new NotImplementedException();
        }







        private void CreateOrChangeEntityLanguage(Category cache)
        {
            var categoryLang = _store.CategoryLangs.FirstOrDefault(r => r.CategoryId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new CategoryLang
                {
                    CategoryId = cache.Id,
                    LanguageId = LangId,
                    
                    Title = cache.Title,
                    SeoDescription = cache.SeoDescription,
                    SeoKeywords = cache.SeoKeywords,
                    SeoText = cache.SeoText
                };
                _store.CategoryLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.SeoDescription = cache.SeoDescription;
                categoryLang.SeoKeywords = cache.SeoKeywords;
                categoryLang.SeoText = cache.SeoText;
            }

        }
    }
}
