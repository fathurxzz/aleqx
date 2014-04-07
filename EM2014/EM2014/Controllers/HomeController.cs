using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2014.Models;
using SiteExtensions;

namespace EM2014.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(string category, string product)
        {
            using (var context = new SiteContext())
            {
                var model = new SiteModel(context, category ?? "", product);
                this.SetSeoContent(model);
                ViewBag.isHomePage = model.Content.IsHomepage;

                ViewBag.Title = model.Title;
                return View(model);
            }
        }

    }
}
