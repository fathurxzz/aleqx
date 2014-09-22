using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Models;
using Newtonsoft.Json;

namespace Kiki.WebSite.Controllers
{
    public class ServiceController : DefaultController
    {
        public ServiceController(ISiteRepository repository) : base(repository)
        {

        }

        public JsonResult Search(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                q = q.ToLower();
                var result = new List<SearchResult>();

                var serviceItems = _repository.GetSearchableServiceItems(q);
                foreach (var serviceItem in serviceItems)
                {
                    if (serviceItem.Title.ToLower().Contains(q))
                    {
                        result.Add(new SearchResult
                        {
                            Name = serviceItem.Service.Name,
                            Price = serviceItem.Price,
                            Title = serviceItem.Title
                        });
                    }
                }

                var json = JsonConvert.SerializeObject(result);

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

    }
}
