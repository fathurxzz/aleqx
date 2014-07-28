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

        public void Save(Category category)
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
