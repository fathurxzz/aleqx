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
        public IEnumerable<Article> GetArticles()
        {
            var articles = _store.Articles.ToList();
            foreach (var article in articles)
            {
                article.CurrentLang = LangId;
                foreach (var articleItem in article.ArticleItems)
                {
                    articleItem.CurrentLang = LangId;
                }
            }
            return articles;
        }

        public Article GetArticle(int id)
        {
            var article = _store.Articles.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Article with id={0} not found", id));
            }
            article.CurrentLang = LangId;
            foreach (var articleItem in article.ArticleItems)
            {
                articleItem.CurrentLang = LangId;
            }
            return article;
        }

        public Article GetArticle(string name)
        {
            var article = _store.Articles.SingleOrDefault(a => a.Name == name);
            if (article == null)
            {
                throw new Exception(string.Format("Article with name={0} not found", name));
            }
            article.CurrentLang = LangId;
            foreach (var articleItem in article.ArticleItems)
            {
                articleItem.CurrentLang = LangId;
            }
            return article;
        }

        public void DeleteArticle(int id, Action<string> deleteImages)
        {
            var article = _store.Articles.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Article with id={0} not found", id));
            }

            while (article.ArticleLangs.Any())
            {
                var artcleLang = article.ArticleLangs.First();
                _store.ArticleLangs.Remove(artcleLang);
            }

            //while (article.ArticleItems.Any())
            //{
                
            //}

            _store.Articles.Remove(article);
            _store.SaveChanges();
        }

        public int AddArticle(Article article)
        {
            if (_store.Articles.Any(c => c.Name == article.Name))
            {
                throw new Exception(string.Format("Article {0} already exists", article.Name));
            }

            _store.Articles.Add(article);

            CreateOrChangeEntityLanguage(article);

            _store.SaveChanges();
            return article.Id;
        }

        public void SaveArticle(Article article)
        {
            var cache = _store.Articles.SingleOrDefault(a => a.Id == article.Id);
            CreateOrChangeEntityLanguage(cache);
            _store.SaveChanges();

        }

        private void CreateOrChangeEntityLanguage(Article cache)
        {
            var categoryLang = _store.ArticleLangs.FirstOrDefault(r => r.ArticleId == cache.Id && r.LanguageId == LangId);
            if (categoryLang == null)
            {
                var entityLang = new ArticleLang
                {
                    ArticleId = cache.Id,
                    LanguageId = LangId,

                    Title = cache.Title,
                    Description = cache.Description,
                    Text = cache.Text,
                };
                _store.ArticleLangs.Add(entityLang);
            }
            else
            {
                categoryLang.Title = cache.Title;
                categoryLang.Description = cache.Description;
                categoryLang.Text = cache.Text;
            }

        }
    }
}
