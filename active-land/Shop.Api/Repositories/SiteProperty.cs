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
        public void SaveSiteProperty(SiteProperty siteProperty)
        {
            _store.SaveChanges();
        }

        public SiteProperty GetSiteProperty(string name)
        {
            return _store.SiteProperties.First(s => s.Name == name);
        }

        public IEnumerable<SiteProperty> GetSiteProperties()
        {
            return _store.SiteProperties;
        }
    }
}
