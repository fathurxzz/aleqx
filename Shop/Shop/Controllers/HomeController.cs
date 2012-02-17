using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var category = new Category {Name = "cat1"
                };
                var product = new Product
                                  {
                                      Name = "p1",

                                  };

                category.Products.Add(product);

                context.AddToCategory(category);

                context.SaveChanges();
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
