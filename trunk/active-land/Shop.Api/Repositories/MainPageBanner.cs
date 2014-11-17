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
        public int AddMainPageBanner(MainPageBanner mainPageBanner)
        {
            _store.MainPageBanners.Add(mainPageBanner);
            _store.SaveChanges();
            return mainPageBanner.Id;
        }

        public void SaveMainPageBanner(MainPageBanner mainPageBanner)
        {
            _store.SaveChanges();
        }

        public MainPageBanner GetMainPageBanner(int id)
        {
            return _store.MainPageBanners.First(s => s.Id == id);
        }

        public IEnumerable<MainPageBanner> GetMainPageBanners()
        {
            return _store.MainPageBanners;
        }

        public void DeleteMainPageBanner(int id, Action<string> deleteImages)
        {
            var mainPageBanner = _store.MainPageBanners.SingleOrDefault(p => p.Id == id);
            if (mainPageBanner == null)
            {
                throw new Exception(string.Format("MainPageBanner with id={0} not found", id));
            }
            deleteImages(mainPageBanner.ImageSource);
            _store.MainPageBanners.Remove(mainPageBanner);
            _store.SaveChanges();
        }
    }
}
