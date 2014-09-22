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
        public IEnumerable<Service> GetServices()
        {
            return _store.Services.ToList();
        }

        public Service GetService(int id)
        {
            var service = _store.Services.SingleOrDefault(c => c.Id == id);
            if (service == null)
            {
                throw new Exception(string.Format("Service with id={0} not found", id));
            }
            return service;
        }

        public Service GetService(string name)
        {
            var service = _store.Services.SingleOrDefault(c => c.Name == name);
            return service;
        }

        public IEnumerable<Service> GetSearchableServices(string query)
        {
            var result = new List<Service>();

            var services = _store.Services.ToList();
            foreach (var service in services)
            {
                if (service.Title.Contains(query))
                {
                    result.Add(service);
                    continue;
                }

                foreach (var serviceItem in service.ServiceItems)
                {
                    if (serviceItem.Title.Contains(query))
                    {
                        result.Add(service);
                        break;
                    }
                }
            }

            foreach (var service in result)
            {
                foreach (var serviceItem in service.ServiceItems)
                {

                }
            }

            return result;
        }

        public IEnumerable<ServiceItem> GetSearchableServiceItems(string query)
        {
            return _store.ServiceItems.Where(serviceItem => serviceItem.Title.ToLower().Contains(query)).ToList();
        }

        public void DeleteService(int id, Action<string> deleteImages)
        {
            var service = _store.Services.SingleOrDefault(a => a.Id == id);
            if (service == null)
            {
                throw new Exception(string.Format("Service with id={0} not found", id));
            }
            deleteImages(service.ImageSource);

            while (service.ServiceItems.Any())
            {
                var si = service.ServiceItems.First();
                _store.ServiceItems.Remove(si);
                _store.SaveChanges();
            }

            _store.Services.Remove(service);
            _store.SaveChanges();
        }

        public void SaveService(Service service)
        {
            _store.SaveChanges();
        }

        public int AddService(Service service)
        {
            _store.Services.Add(service);
            _store.SaveChanges();
            return service.Id;
        }
    }
}
