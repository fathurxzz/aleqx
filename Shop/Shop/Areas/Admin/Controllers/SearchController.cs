using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Admin/Search/

        public ActionResult Index(int? productId, string productName, string articul)
        {
            var result = new List<Product>();
            
            if (productId.HasValue || !string.IsNullOrEmpty(productName)||!string.IsNullOrEmpty(articul))
            {
                using (var context = new ShopContainer())
                {
                    if (productId.HasValue)
                    {
                        var product = context.Product.Include("Brand").Include("Category").FirstOrDefault(p => p.Id == productId);
                        if (product != null)
                            result.Add(product);
                    }
                    
                    if(!string.IsNullOrEmpty(articul))
                    {
                        var product = context.Product.Include("Brand").Include("Category").FirstOrDefault(p => p.Articul == articul);
                        if (product != null)
                            result.Add(product);
                    }

                    if(!string.IsNullOrEmpty(productName))
                    {
                        productName = productName.Trim();
                        if (!string.IsNullOrEmpty(productName))
                        {
                            var products = context.Product.Include("Brand").Include("Category").Where(p => p.Title.Contains(productName)).ToList();
                            foreach (var product in products.Where(product => !result.Contains(product)))
                            {
                                result.Add(product);
                            }
                        }
                    }
                }
            }
            return View(result);
        }

    }
}
