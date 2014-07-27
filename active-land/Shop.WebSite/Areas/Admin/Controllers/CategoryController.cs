using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.EntityFramework;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {

        private readonly ShopContext _context;

        public CategoryController(ShopContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            foreach (var category in categories)
            {
                category.CurrentLang = CurrentLang.Id;
            }
            return View(categories);
        }

    }
}
