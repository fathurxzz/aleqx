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
        public ContentItem GetContentItem(int id)
        {
            var contentItem = _store.ContentItems.SingleOrDefault(ci => ci.Id == id);
            if (contentItem == null)
            {
                throw new Exception(string.Format("ContentItem with id={0} not found", id));
            }
            contentItem.CurrentLang = LangId;
            return contentItem;
        }

        public void DeleteContntItem(int id, Action<string> deleteImages)
        {
            var contentItem = _store.ContentItems.SingleOrDefault(ai => ai.Id == id);
            if (contentItem == null)
            {
                throw new Exception(string.Format("ContentItem with id={0} not found", id));
            }
            while (contentItem.ContentItemLangs.Any())
            {
                var lang = contentItem.ContentItemLangs.First();
                _store.ContentItemLangs.Remove(lang);
            }
            while (contentItem.ContentItemImages.Any())
            {
                var image = contentItem.ContentItemImages.First();
                deleteImages(image.ImageSource);
                _store.ContentItemImages.Remove(image);
            }
            _store.ContentItems.Remove(contentItem);
            _store.SaveChanges();
        }

        public void SaveContentItem(ContentItem contentItem)
        {
            var cache = _store.ContentItems.Single(c => c.Id == contentItem.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public int AddContentItem(ContentItem contentItem)
        {
            _store.ContentItems.Add(contentItem);
            CreateOrChangeEntityLanguage(contentItem);
            _store.SaveChanges();
            return contentItem.Id;
        }

        private void CreateOrChangeEntityLanguage(ContentItem cache)
        {
            var categoryLang = _store.ContentItemLangs.FirstOrDefault(r => r.ContentItemId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ContentItemLang
                {
                    ContentItemId = cache.Id,
                    LanguageId = LangId,

                    Text = cache.Text,
                };
                _store.ContentItemLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Text = cache.Text;
            }

        }
    }
}
