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
        public IEnumerable<GalleryImage> GetGalleryImages()
        {
            return _store.GalleryImages.ToList();
        }

        public void DeleteGalleryImage(int id, Action<string> deleteImages)
        {
            var galleryImage = _store.GalleryImages.SingleOrDefault(si => si.Id == id);
            if (galleryImage == null)
            {
                throw new Exception(string.Format("GalleryImage with id={0} not found", id));
            }
            deleteImages(galleryImage.ImageSource);
            _store.GalleryImages.Remove(galleryImage);
            _store.SaveChanges();
        }

        public int AddGalleryImage(GalleryImage galleryImage)
        {
            _store.GalleryImages.Add(galleryImage);
            _store.SaveChanges();
            return galleryImage.Id;
        }
    }
}
