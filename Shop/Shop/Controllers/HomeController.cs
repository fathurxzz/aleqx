using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;
using Shop.Helpers;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string id)
        {
            using (var context = new ShopContainer())
            {
                SiteViewModel model = new SiteViewModel(context, id);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View(model);
            }
        }

        public ActionResult NotFound()
        {
            using (var context = new ShopContainer())
            {
                SiteViewModel model = new SiteViewModel(context, null);
                ViewBag.MainMenu = model.MainMenu;
                return View(model);
            }
        }


        
    }
}
