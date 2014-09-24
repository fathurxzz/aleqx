using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Models;
using ContentType = Kiki.DataAccess.ContentType;

namespace Kiki.WebSite.Controllers
{
    public class HomeController : DefaultController
    {
        public HomeController(ISiteRepository repository)
            : base(repository)
        {

        }

        public ActionResult Index(string id)
        {
            var model = new SiteModel(_repository, id, CurrentLangCode);
            this.SetSeoContent(model);
            switch ((ContentType) model.Content.ContentType)
            {
                case ContentType.Gallery:
                    return View("Gallery", model);
                case ContentType.Articles:
                    return View("Articles", model);
                case ContentType.Sales:
                    return View("Sales", model);
                case ContentType.Contacts:
                    return View("Contacts", model);
                case ContentType.Services:
                    return View("Services", model);

                default:
                    ViewBag.isHomePage = true;
                    return View(model);
            }
        }

        public ActionResult ArticleDetails(string id, string contentName)
        {
            var model = new ArticleModel(_repository, contentName, id,CurrentLangCode);

            return View(model);
        }

        public ActionResult SaleDetails(string id, string contentName)
        {
            var model = new SaleModel(_repository, contentName, id, CurrentLangCode);

            return View(model);
        }

        public ActionResult ServiceDetails(string id, string contentName)
        {
            var model = new ServiceModel(_repository, contentName, id, CurrentLangCode);
            return View(model);
        }
    }
}
