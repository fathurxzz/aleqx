using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Models;

namespace EM2013.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Create()
        {
            using (var context = new SiteContext())
            {
                var max = context.Category.Max(c => c.SortOrder);
                return View(new Category {SortOrder = max + 1});
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new SiteContext())
            {
                var category = new Category();
                TryUpdateModel(category, new[] { "Name", "Title", "Description", "SeoDescription", "SeoKeywords", "SortOrder" });
                context.AddToCategory(category);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category = category.Name });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.First(c => c.Id == id);
                TryUpdateModel(category, new[] { "Name", "Title", "Description", "SeoDescription", "SeoKeywords", "SortOrder" });
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", category = category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var category = context.Category.Include("Products").First(c => c.Id == id);
                if (!category.Products.Any())
                {
                    context.DeleteObject(category);
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
