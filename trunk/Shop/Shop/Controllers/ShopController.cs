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

        private void SetSeoContent(SiteViewModel model)
        {
            ViewBag.Title = model.Title;
            ViewBag.SeoDescription = model.SeoDescription;
            ViewBag.SeoKeywords = model.SeoKeywords;
        }

        public ActionResult Categories(string id)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, id, null);
                SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult ProductDetails(string id, string category)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, category, id);
                SetSeoContent(model);
                return View(model);
            }
        }

    }
}
