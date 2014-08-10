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
        public ArticleItem GetArticleItem(int id)
        {
            var articleItem = _store.ArticleItems.SingleOrDefault(ai => ai.Id == id);
            if (articleItem == null)
            {
                throw new Exception(string.Format("ArticleItem with id={0} not found", id));
            }
            articleItem.CurrentLang = LangId;
            return articleItem;
        }

        public void DeleteArticleItem(int id, Action<string> deleteImages)
        {
            var articleItem = _store.ArticleItems.SingleOrDefault(ai => ai.Id == id);
            if (articleItem == null)
            {
                throw new Exception(string.Format("ArticleItem with id={0} not found", id));
            }
            while (articleItem.ArticleItemLangs.Any())
            {
                var lang = articleItem.ArticleItemLangs.First();
                _store.ArticleItemLangs.Remove(lang);
            }
            while (articleItem.ArticleItemImages.Any())
            {
                var image = articleItem.ArticleItemImages.First();
                deleteImages(image.ImageSource);
                _store.ArticleItemImages.Remove(image);
            }
            _store.ArticleItems.Remove(articleItem);
            _store.SaveChanges();
        }

        public void SaveArticleItem(ArticleItem articleItem)
        {
            var cache = _store.ArticleItems.Single(c => c.Id == articleItem.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public int AddArticleItem(ArticleItem articleItem)
        {
            _store.ArticleItems.Add(articleItem);
            CreateOrChangeEntityLanguage(articleItem);
            _store.SaveChanges();
            return articleItem.Id;
        }

        private void CreateOrChangeEntityLanguage(ArticleItem cache)
        {
            var categoryLang = _store.ArticleItemLangs.FirstOrDefault(r => r.ArticleItemId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ArticleItemLang
                {
                    ArticleItemId = cache.Id,
                    LanguageId = LangId,

                    Text = cache.Text,
                };
                _store.ArticleItemLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Text = cache.Text;
            }

        }
    }
}
