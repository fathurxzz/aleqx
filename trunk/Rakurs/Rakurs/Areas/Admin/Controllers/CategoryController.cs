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

        public ActionResult Add(int? parentId)
        {
            ViewData["parentId"] = parentId;
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

                TryUpdateModel(category, new[] { "Title", "SortOrder" });
                category.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToCategory(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
