using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryAttributeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new CatalogueContainer())
            {
                ViewBag.Categories = context.Category.ToList();
                ViewBag.Projects = context.Project.ToList();
                var pa = context.CategoryAttribute.ToList();
                return View(pa);
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
                using (var context = new CatalogueContainer())
                {
                    var attribute = new CategoryAttribute();
                    TryUpdateModel(attribute, new[] { "Title", "Name" });
                    context.AddToCategoryAttribute(attribute);
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
            using (var context = new CatalogueContainer())
            {
                var attribute = context.CategoryAttribute.First(a => a.Id == id);
                return View(attribute);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var attribute = context.CategoryAttribute.First(a => a.Id == id);
                    TryUpdateModel(attribute, new[] { "Title", "Name" });
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
            using (var context = new CatalogueContainer())
            {
                var attribute = context.CategoryAttribute.First(a => a.Id == id);
                attribute.Categories.Clear();
                attribute.Brands.Clear();
                context.DeleteObject(attribute);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
