using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;

namespace Leo.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            return View(new Category{SortOrder = 0});
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var category = new Category();
                TryUpdateModel(category, new[] { "Name", "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
                using (var context = new SiteContainer())
                {
                    context.AddToCategory(category);
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Catalogue", new { Area = "", id = category.Name });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
    }
}
