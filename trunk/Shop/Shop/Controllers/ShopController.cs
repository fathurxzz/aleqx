using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers;
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
                ShopViewModel model = new ShopViewModel(context, id, null, null, null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View("Products", model);
            }
        }

        public ActionResult Brands(string id)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context,null, id,null, null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View("Products", model);
            }
        }

        public ActionResult Tags(string id)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, null, null, id, null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View("Products", model);
            }
        }

        public ActionResult ProductDetails(string id, string category)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, category, null, null, id);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View(model);
            }
        }

        

    }
}
