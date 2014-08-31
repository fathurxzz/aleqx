using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.Api.Repositories
{
    public partial class SiteRepository : ISiteRepository
    {
        public IEnumerable<SiteImage> GetSiteImages(ImageType imageType)
        {
            return _store.SiteImages.Where(i=>i.ImageType==(int)imageType).ToList();
        }

        public SiteImage GetSiteImage(int id)
        {
            return _store.SiteImages.FirstOrDefault(si => si.Id == id);
        }

        public void DeleteSiteImage(int id, Action<string> deleteImages)
        {
            var siteImage = _store.SiteImages.SingleOrDefault(si => si.Id == id);
            if (siteImage == null)
            {
                throw new Exception(string.Format("SiteImage with id={0} not found", id));
            }
            deleteImages(siteImage.ImageSource);
            _store.SiteImages.Remove(siteImage);
            _store.SaveChanges();
        }

        public int AddSiteImage(SiteImage siteImage)
        {
            _store.SiteImages.Add(siteImage);
            _store.SaveChanges();
            return siteImage.Id;
        }

        public void SaveSiteImage(SiteImage siteImage)
        {
            var cache = _store.Contents.Single(c => c.Id == siteImage.Id);
            _store.SaveChanges();
        }
    }
}
