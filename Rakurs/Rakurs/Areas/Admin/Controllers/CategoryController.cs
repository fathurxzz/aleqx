using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Models;

namespace Rakurs.Areas.Admin.Controllers
{
    //[Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/

        public ActionResult Add(int? id)
        {
            ViewData["parentId"] = id;
            return View(new Category{SortOrder = 0});
        }

        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var category = new Category();

                if (!string.IsNullOrEmpty(form["parentId"]))
                {
                    int parentId = Convert.ToInt32(form["parentId"]);
                    var parent = context.Category.Where(c => c.Id == parentId).First();
                    category.Parent = parent;
                }

                TryUpdateModel(category, new[] {"Name", "Title", "SortOrder" });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToCategory(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new {Area = "", id = ""});
        }


        public ActionResult Edit(int id)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Where(c => c.Id == id).First();
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Category model, FormCollection form)
        {
            using (var context = new StructureContainer())
            {
                var category = context.Category.Where(c => c.Id == model.Id).First();

                TryUpdateModel(category, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "SortOrder"
                                            });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { Area = "", id = "" });
            }
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
