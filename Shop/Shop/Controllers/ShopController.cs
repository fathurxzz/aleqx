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

        public ActionResult Categories(string id, int? page)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, id, null, null, null,page, true);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.TotalCount = model.TotalProductsCount;
                ViewBag.Page = page ?? 0;
                ViewBag.CategoryId = id;
                return View("Products", model);
            }
        }

        public ActionResult Brands(string id, int? page)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context,null, id,null, null,null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View("Products", model);
            }
        }

        public ActionResult Tags(string id, int? page)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, null, null, id, null,null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View("Products", model);
            }
        }

        public ActionResult ProductDetails(string id, string category)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, category, null, null, id,null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                return View(model);
            }
        }


        public ActionResult Search(string q)
        {
            using (var context = new ShopContainer())
            {
                ShopViewModel model = new ShopViewModel(context, null, null, null, null,null);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;

                if(!string.IsNullOrEmpty(q))
                {
                    var query = q.Trim();
                    if(!string.IsNullOrEmpty(query))
                    {
                        var products = context.Product.Include("ProductImages").Where(p => p.Title.Contains(query)).ToList();
                        
                        if(products.Count==0)
                        {
                            ViewBag.Message = "Ничего не найдено";
                        }
                        else
                        {
                            model.Products.AddRange(products);    
                        }
                    }
                }

                return View("Products", model);
            }
        }
    }
}
