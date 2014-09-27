using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Entities;
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

        //public JsonResult Search(string q)
        //{
        //    if (!string.IsNullOrEmpty(q))
        //    {
        //        q = q.ToLower();
        //        var parentResult = new List<ParentResult>();
        //        var result = new List<SearchResult>();

        //        var serviceItems = _repository.GetSearchableServiceItems(q);
        //        foreach (var serviceItem in serviceItems)
        //        {
        //            if (serviceItem.Title.ToLower().Contains(q))
        //            {
        //                result.Add(new SearchResult
        //                {
        //                    Name = serviceItem.Service.Name,
        //                    Price = serviceItem.Price,
        //                    Title = serviceItem.Title
        //                });
        //            }
        //        }

        //        var json = JsonConvert.SerializeObject(result);

        //        return Json(json, JsonRequestBehavior.AllowGet);
        //    }
        //    return null;
        //}

        public JsonResult Search(string q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                q = q.ToLower();
                var result = new List<SearchResult>();

                
                var services = _repository.GetSearchableData(q).ToList();

                foreach (var service in services)
                {
                    var sr = new SearchResult
                    {
                        Children = new List<SearchResult>(),
                        Title = service.Title,
                        Name = service.Name
                    };

                    foreach (var serviceItem in service.ServiceItems)
                    {
                        if ((CurrentLangCode == "en" && !string.IsNullOrEmpty(serviceItem.TitleEng) && serviceItem.TitleEng.ToLower().Contains(q)) || (CurrentLangCode == "ru" && !string.IsNullOrEmpty(serviceItem.Title) && serviceItem.Title.ToLower().Contains(q)))
                        {
                            sr.Children.Add(new SearchResult()
                            {
                                Name = serviceItem.Service.Name,
                                Price = serviceItem.Price,
                                Title = (CurrentLangCode == "en" ? serviceItem.TitleEng : serviceItem.Title)??""
                            });
                        }
                    }
                    if (sr.Children.Any())
                    {
                        result.Add(sr);
                        continue;
                    }
                    if (sr.Title.ToLower().Contains(q))
                    {
                        result.Add(sr);
                    }
                }

                var json = JsonConvert.SerializeObject(result);

                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return null;
        }


        public JsonResult Subscribe(string email)
        {
            try
            {
                var subscriber = new Subscriber() { Email = email };
                int id = _repository.AddSubscriber(subscriber);
                return Json(new {errorCode = 0}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
            }
            return Json(new { errorCode = 1, }, JsonRequestBehavior.AllowGet);
        }

    }
}
