using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        //
        // GET: /Shop/

        public ActionResult Categories(string id)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, id, null);
                ViewBag.Title = model.Title;
                return View(model);
            }
        }

        public ActionResult Products(string id)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, null, id);
                ViewBag.Title = model.Title;
                return View(model);
            }
        }

    }
}
