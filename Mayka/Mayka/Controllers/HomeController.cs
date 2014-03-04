using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Helpers;
using Mayka.Models;
using SiteExtensions;

namespace Mayka.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index(string id)
        {
            using (var context = new SiteContext())
            {
                var model = new SiteModel(context, id);
                ViewBag.isHomePage = model.Content.ContentType==(int)ContentType.HomePage;
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Gallery(string id)
        {

            using (var context = new SiteContext())
            {
                var model = new SiteModel(context, id);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Products(string id)
        {

            using (var context = new SiteContext())
            {
                var model = new SiteModel(context, id);
                this.SetSeoContent(model);
                return View(model);
            }
        }



    }
}
