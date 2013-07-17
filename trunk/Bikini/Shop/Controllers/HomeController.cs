using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using SiteExtensions;

namespace Shop.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {
            using (var context = new ShopContainer())
            {
                var model = new SiteModel(context, id);
                ViewBag.IsHomePage = model.IsHomePage;
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }
    }
}
