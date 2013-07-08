using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class CategoryBrandController : AdminController
    {
        public ActionResult Create(int category)
        {
            using (var context = new SiteContainer())
            {
                var cat = context.Category.First(c => c.Id == category);
                int maxSortOrder = context.CategoryBrand.Where(c => c.CategoryId == cat.Id).Max(c => (int?)c.SortOrder) ?? 0;
                var categoryBrand = new CategoryBrand
                {
                    SortOrder = maxSortOrder + 1,
                    Category = cat
                };
                ViewBag.CategoryName = category;
                return View(categoryBrand);
            }
        }

        [HttpPost]
        public ActionResult Create(CategoryBrand model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == model.CategoryId);
                    var cache = new CategoryBrand
                                    {
                                        Category = category,
                                        SortOrder = model.SortOrder,
                                        Name = SiteHelper.UpdatePageWebName(model.Name),
                                        Title = model.Title
                                    };
                    context.AddToCategoryBrand(cache);
                    context.SaveChanges();

                    return RedirectToAction("Details", "Category", new { area = "FactoryCatalogue", id = category.Name });
                }
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
                var brand = context.CategoryBrand.Include("Category").First(b => b.Id == id);
                return View(brand);
            }
        }

        [HttpPost]
        public ActionResult Edit(CategoryBrand model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var brand = context.CategoryBrand.Include("Category").First(b => b.Id == model.Id);
                    TryUpdateModel(brand, new[] { "SortOrder", "Title" });
                    brand.Name = SiteHelper.UpdatePageWebName(model.Name);
                    context.SaveChanges();
                    return RedirectToAction("Details", "Category", new { area = "FactoryCatalogue", id = brand.Category.Name });
                }
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
                var brand = context.CategoryBrand.Include("Category").Include("CategoryBrandItems").First(b => b.Id == id);
                var categoryName = brand.Category.Name;

                foreach (var categoryBrandItem in brand.CategoryBrandItems)
                {
                    categoryBrandItem.CategoryBrandItemLangs.Load();
                }

                while (brand.CategoryBrandItems.Any())
                {
                    var cbi = brand.CategoryBrandItems.First();
                    while (cbi.CategoryBrandItemLangs.Any())
                    {
                        var cbil = cbi.CategoryBrandItemLangs.First();
                        context.DeleteObject(cbil);
                    }
                    context.DeleteObject(cbi);
                }

                context.DeleteObject(brand);
                context.SaveChanges();
                return RedirectToAction("Details", "Category", new { area = "FactoryCatalogue", id = categoryName });
            }
        }


    }
}
