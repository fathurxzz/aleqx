using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var categories = context.Category.ToList();
                return View(categories);
            }
        }

        //
        // GET: /Admin/Category/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = new Category
                    {
                        Name = form["Name"],
                        SeoDescription = form["SeoDescription"],
                        SeoKeywords = form["SeoKeywords"],
                        SortOrder = Convert.ToInt32(form["SortOrder"])
                    };
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

        //
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    TryUpdateModel(category, new[] { "Name", "SeoDescription", "SeoKeywords", "SortOrder" });
                    context.SaveChanges();
                }
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

        public ActionResult Attributes(int id)
        {
            using (var context = new ShopContainer())
            {
                ViewBag.Category = context.Category.First(c => c.Id == id);
                var attributes = context.ProductAttribute.ToList();
                return View(attributes);
            }
        }
    }
}
