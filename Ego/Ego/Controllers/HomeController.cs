using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ego.Models;
using SiteExtensions;

namespace Ego.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id, int? page)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context, id, page);
                ViewBag.IsHomePage = model.IsHomePage;
                ViewBag.Page = page ?? 0;
                ViewBag.TotalCount = model.TotalProductsCount;
                this.SetSeoContent(model);
                return View(model);
            }
        }
    }
}
