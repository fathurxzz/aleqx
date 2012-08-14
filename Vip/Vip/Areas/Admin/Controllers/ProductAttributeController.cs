using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    public class ProductAttributeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var pa = context.ProductAttribute.ToList();
                return View(pa);
            }
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var attribute = new ProductAttribute();
                    TryUpdateModel(attribute, new[] { "Title" });
                    context.AddToProductAttribute(attribute);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var attribute = context.ProductAttribute.First(a => a.Id == id);
                return View(attribute);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var attribute = context.ProductAttribute.First(a => a.Id == id);
                    TryUpdateModel(attribute, new[] { "Title" });
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var attribute = context.ProductAttribute.First(a => a.Id == id);
                attribute.Categories.Clear();
                attribute.Products.Clear();
                context.DeleteObject(attribute);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
