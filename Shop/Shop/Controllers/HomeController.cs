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
    
        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                SiteViewModel model = new SiteViewModel(context);
                this.SetSeoContent(model);
                return View(model);
            }
        }
    }
}
