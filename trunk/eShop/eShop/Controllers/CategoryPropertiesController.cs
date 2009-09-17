using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using eShop.Models;

namespace eShop.Controllers
{
    public class CategoryPropertiesController : Controller
    {
        //
        // GET: /CategoryProperties/

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(int categoryId)
        { 
            using (ShopStorage context = new ShopStorage())
            {
                List<CategoryProperties> categoryProperties = (from categoryProperty in context.CategoryProperties where categoryProperty.Category.Id == categoryId select categoryProperty).ToList();
                return View(categoryProperties);
            }

        }

    }
}
