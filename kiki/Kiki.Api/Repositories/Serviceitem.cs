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
        public ServiceItem GetServiceItem(int id)
        {
            var serviceItem = _store.ServiceItems.SingleOrDefault(c => c.Id == id);
            if (serviceItem == null)
            {
                throw new Exception(string.Format("ServiceItem with id={0} not found", id));
            }
            return serviceItem;
        }

        public void DeleteServiceItem(int id)
        {
            var serviceItem = _store.ServiceItems.SingleOrDefault(a => a.Id == id);
            if (serviceItem == null)
            {
                throw new Exception(string.Format("ServiceItem with id={0} not found", id));
            }

            _store.ServiceItems.Remove(serviceItem);
            _store.SaveChanges();
        }

        public void SaveServiceItem(ServiceItem serviceItem)
        {
            _store.SaveChanges();
        }

        public int AddServiceItem(ServiceItem serviceItem)
        {
            _store.ServiceItems.Add(serviceItem);
            _store.SaveChanges();
            return serviceItem.Id;
        }
    }
}
