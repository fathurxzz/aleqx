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
        public ContentItemImage GetContentItemImage(int id)
        {
            var contentItemImage = _store.ContentItemImages.SingleOrDefault(ai => ai.Id == id);
            if (contentItemImage == null)
            {
                throw new Exception(string.Format("ContentItemImage with id={0} not found", id));
            }
            return contentItemImage;
        }

        public void DeleteContentItemImage(int id, Action<string> deleteImages)
        {
            var contentItemImage = _store.ContentItemImages.SingleOrDefault(ai => ai.Id == id);
            if (contentItemImage == null)
            {
                throw new Exception(string.Format("ContentItemImage with id={0} not found", id));
            }
            deleteImages(contentItemImage.ImageSource);
            _store.ContentItemImages.Remove(contentItemImage);
            _store.SaveChanges();
        }
    }
}
