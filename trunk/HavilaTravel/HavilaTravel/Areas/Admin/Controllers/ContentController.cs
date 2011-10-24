using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "MenuTitle",
                                                "PageTitle",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new {id = content.Name, area = ""});
            }
        }

        public ActionResult AddSubmenu(int id)
        {
            ViewBag.parentId = id;
            return View(new Content());
        }

        [HttpPost]
        public ActionResult AddSubmenu(int parentId, FormCollection form)
        {
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
