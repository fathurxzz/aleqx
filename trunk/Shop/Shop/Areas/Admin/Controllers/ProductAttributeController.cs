using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class ProductAttributeController : Controller
    {
        //
        // GET: /Admin/ProductAttribute/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var productAttributes = context.ProductAttribute.ToList();
                return View(productAttributes);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var productAttribute = new ProductAttribute
                    {
                        Name = form["Name"],
                        SortOrder = Convert.ToInt32(form["SortOrder"]),
                        ValueType = form["ValueType"],
                        ShowInCommonView = Convert.ToBoolean(form["ShowInCommonView"])
                    };
                    context.AddToProductAttribute(productAttribute);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
