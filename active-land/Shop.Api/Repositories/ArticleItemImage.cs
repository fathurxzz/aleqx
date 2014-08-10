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
        public ArticleItemImage GetArticleItemImage(int id)
        {
            var articleItemImage = _store.ArticleItemImages.SingleOrDefault(ai => ai.Id == id);
            if (articleItemImage == null)
            {
                throw new Exception(string.Format("ArticleItemImage with id={0} not found", id));
            }
            return articleItemImage;
        }

        public void DeleteArticleItemImage(int id, Action<string> deleteImages)
        {
            var articleItemImage = _store.ArticleItemImages.SingleOrDefault(ai => ai.Id == id);
            if (articleItemImage == null)
            {
                throw new Exception(string.Format("ArticleItemImage with id={0} not found", id));
            }
            deleteImages(articleItemImage.ImageSource);
            _store.ArticleItemImages.Remove(articleItemImage);
            _store.SaveChanges();
        }
    }
}
