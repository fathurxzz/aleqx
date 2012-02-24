using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class CategoriesController : Controller
    {
        //
        // GET: /Categories/

        public ActionResult Index(int id)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context,id);
                return View(model);
            }
        }

    }
}
