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


        public IEnumerable<Content> GetContents()
        {
            var contents = _store.Contents.ToList();
            foreach (var content in contents)
            {
                content.CurrentLang = LangId;
            }
            return contents;
        }

        public Content GetContent(int id)
        {
            var content = _store.Contents.SingleOrDefault(c => c.Id == id);
            if (content == null)
            {
                throw new Exception(string.Format("Content with id={0} not found", id));

            }
            content.CurrentLang = LangId;
            return content;
        }

        public Content GetContent(string name)
        {
            var content = _store.Contents.SingleOrDefault(c => c.Name == name);
            if (content == null)
            {
                throw new Exception(string.Format("Content with name={0} not found", name));
            }
            content.CurrentLang = LangId;
            return content;
        }

        public void DeleteContent(int id)
        {
            var content = _store.Contents.SingleOrDefault(c => c.Id == id);
            if (content == null)
            {
                throw new Exception(string.Format("Content with id={0} not found", id));

            }

            _store.Contents.Remove(content);
            _store.SaveChanges();
        }

        public void SaveContent(Content content)
        {
            var cache = _store.Contents.Single(c => c.Id == content.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();
        }

        public int AddContent(Content content)
        {
            if (_store.Contents.Any(c => c.Name == content.Name))
            {
                throw new Exception(string.Format("Content {0} already exists", content.Name));
            }
            _store.Contents.Add(content);
            CreateOrChangeEntityLanguage(content);
            _store.SaveChanges();
            return content.Id;
        }



        private void CreateOrChangeEntityLanguage(Content cache)
        {
            var categoryLang = _store.ContentLangs.FirstOrDefault(r => r.ContentId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ContentLang
                {
                    ContentId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    Text = cache.Text,
                    SeoDescription = cache.SeoDescription,
                    SeoKeywords = cache.SeoKeywords,
                    SeoText = cache.SeoText
                };
                _store.ContentLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.SeoDescription = cache.SeoDescription;
                categoryLang.SeoKeywords = cache.SeoKeywords;
                categoryLang.SeoText = cache.SeoText;
                categoryLang.Text = cache.Text;

            }

        }
    }
}
