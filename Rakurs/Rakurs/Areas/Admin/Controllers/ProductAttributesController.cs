using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductAttributesController : Controller
    {
        //
        // GET: /Admin/ProductAttributes/

        public ActionResult Index()
        {
            using (var context = new StructureContainer())
            {
                var attributes = context.ProductAttribute.ToList();
                return View(attributes);
            }
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                var productAttribute = context.ProductAttribute.First(pa => pa.Id == id);
                return View(productAttribute);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model, FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var productAttribute = context.ProductAttribute.First(pa => pa.Id == model.Id);
                TryUpdateModel(productAttribute, new[] { "Title" });
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Create(ProductAttribute model)
        {
            using (var context = new StructureContainer())
            {
                var pa = new ProductAttribute();
                TryUpdateModel(pa, new[] { "Title" });
                context.AddToProductAttribute(pa);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (var context = new StructureContainer())
            {
                var productAttribute = context.ProductAttribute
                    .Include("Categories")
                    .Include("Products")
                    .First(pa => pa.Id == id);

                productAttribute.Products.Clear();
                productAttribute.Categories.Clear();

                context.DeleteObject(productAttribute);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
