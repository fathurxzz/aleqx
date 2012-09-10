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
                    return parent != null
                        ? RedirectToAction("Index", "Catalogue", new { area = "", category = parent.Name, subcategory = category.Name })
                        : RedirectToAction("Index", "Catalogue", new { area = "", category = category.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Category/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
