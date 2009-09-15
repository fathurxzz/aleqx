using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using eShop.Models;

namespace eShop.Controllers
{
    public class CategoriesController : Controller
    {
        public ActionResult Index()
        {
            using (ShopStorage context = new ShopStorage())
            {
                List<Category> categories = (from category in context.Categories where category.Enabled==true select category).ToList();

                return View(categories);
            }
        }
    }
}
