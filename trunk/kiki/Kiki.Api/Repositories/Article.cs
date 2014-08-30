using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.Api.Repositories
{
    public partial class SiteRepository : ISiteRepository
    {
        public IEnumerable<Article> GetArticles(bool showOnlyActive = false)
        {
            return _store.Articles.ToList();
        }

        public Article GetArticle(int id)
        {
            var article = _store.Articles.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Article with id={0} not found", id));
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
            return article;
        }

        public void DeleteArticle(int id, Action<string> deleteImages)
        {
            var article = _store.Articles.SingleOrDefault(a => a.Id == id);
            if (article == null)
            {
                throw new Exception(string.Format("Article with id={0} not found", id));
            }

            deleteImages(article.ImageSource);
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
            _store.SaveChanges();
            return article.Id;
        }

        public void SaveArticle(Article article)
        {
            var cache = _store.Articles.SingleOrDefault(a => a.Id == article.Id);
            _store.SaveChanges();
        }
    }
}
