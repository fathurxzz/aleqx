using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poggen.Models;

namespace Poggen.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private static string GetControllerNameByCategoryType(int categoryType)
        {
            switch (categoryType)
            {
                case 1:
                    return "Brands";
                case 2:
                    return "Project";
            }
            return "Catalogue";
        }

        public ActionResult Create(int categoryType, int? parentId)
        {
            if (parentId.HasValue)
                ViewBag.ParentId = parentId.Value;
            ViewBag.CategoryType = categoryType;
            return View(new Category());
        }

        [HttpPost]
        public ActionResult Create(int? parentId, int categoryType, FormCollection form)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = new Category { MainPage = false, CategoryType = categoryType };
                    Category parent = null;
                    TryUpdateModel(category, new[] { "Name", "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
                    category.Text = HttpUtility.HtmlDecode(form["Text"]);
                    if (parentId.HasValue)
                    {
                        parent = context.Category.First(c => c.Id == parentId);
                        parent.Children.Add(category);
                    }
                    else
                    {
                        context.AddToCategory(category);
                    }
                    context.SaveChanges();

                    string controllerName = GetControllerNameByCategoryType(categoryType);


                    return parent != null
                        ? RedirectToAction("Index", controllerName, new { area = "", category = parent.Name, subcategory = category.Name })
                        : RedirectToAction("Index", controllerName, new { area = "", category = category.Name });
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
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {

                using (var context = new SiteContainer())
                {
                    var category = context.Category.Include("Parent").First(c => c.Id == id);

                    TryUpdateModel(category, new[] { "Name", "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
                    category.Text = HttpUtility.HtmlDecode(form["Text"]);

                    context.SaveChanges();

                    if (category.MainPage)
                        return RedirectToAction("Index", "Catalogue", new { area = "" });
                    string controllerName = GetControllerNameByCategoryType(category.CategoryType);
                    return category.Parent != null
                     ? RedirectToAction("Index", controllerName, new { area = "", category = category.Parent.Name, subcategory = category.Name })
                     : RedirectToAction("Index", controllerName, new { area = "", category = category.Name });
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
                var category = context.Category.Include("Children").First(c => c.Id == id);
                var categoryType = category.CategoryType;
                if (!category.Children.Any())
                {
                    context.DeleteObject(category);
                    context.SaveChanges();
                }
                string controllerName = GetControllerNameByCategoryType(categoryType);
                return RedirectToAction("Index", controllerName, new { area = "" });
            }
        }
    }
}
