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
        public void SaveShopSettings(ShopSetting shopSettings)
        {
            _store.SaveChanges();
        }

        public ShopSetting GetShopSetting(string key)
        {
            return _store.ShopSettings.First(s => s.Key == key);
        }

        public ShopSetting GetShopSetting(int id)
        {
            return _store.ShopSettings.First(s => s.Id == id);
        }

        public IEnumerable<ShopSetting> GetShopSettings()
        {
            return _store.ShopSettings;
        }
    }
}
