using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Models
{
    public class ServiceModel : SiteModel
    {
        public Service Service { get; set; }
        public string Query { get; set; }
        public List<ServiceItem> SearchableServiceItems { get; set; }
        //public List<ServiceItem> OtherServiceItems { get; set; }


        public ServiceModel(ISiteRepository repository, string contentName, string serviceId,string lang, string query) : base(repository, contentName, lang)
        {
            Query = query;
            Service = Services.First(s => s.Name == serviceId);
            SearchableServiceItems = new List<ServiceItem>();
            //OtherServiceItems = new List<ServiceItem>();
            if (!string.IsNullOrEmpty(query))
            {
                foreach (
                    var serviceItem in
                        Service.ServiceItems.Where(serviceItem => serviceItem.Title.ToLower().Contains(query.ToLower()))
                    )
                {
                    SearchableServiceItems.Add(serviceItem);
                }
            }
        }
    }
}