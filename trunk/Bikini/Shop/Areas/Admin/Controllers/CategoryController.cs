using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                return View();
            }
        }

        public ActionResult Create()
        {
            using (var context = new ShopContainer())
            {
                int maxSortOrder = context.Category.Max(c => (int?)c.SortOrder) ?? 0;
                var category = new Category
                {
                    SortOrder = maxSortOrder + 1,
                };
                return View(category);
            }
        } 

        [HttpPost]
        public ActionResult Create(Category model)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = new Category();
                    TryUpdateModel(category, new[] { "Name", "SeoDescription", "SeoKeywords", "SortOrder", "Title" });
                    context.AddToCategory(category);
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
            using (var context = new ShopContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Category model)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    TryUpdateModel(category, new[] { "Name", "SeoDescription", "SeoKeywords", "SortOrder", "Title" });
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
            using (var context = new ShopContainer())
            {
                var category = context.Category.Include("Products").First(c => c.Id == id);
                category.Products.Clear();
                context.DeleteObject(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        
    }
}
