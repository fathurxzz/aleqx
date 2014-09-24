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

        public ServiceModel(ISiteRepository repository, string contentName, string serviceId,string lang) : base(repository, contentName,lang)
        {
            Service = Services.First(s => s.Name == serviceId);
        }
    }
}