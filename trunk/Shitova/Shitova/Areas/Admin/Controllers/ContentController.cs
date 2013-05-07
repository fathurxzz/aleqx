using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shitova.Models;

namespace Shitova.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                TryUpdateModel(content, new[] {"Title", "SortOrder", "SeoDescription", "SeoKeywords"});
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new {area = "", id = content.Name});
            }
        }


        public ActionResult AddTextBlock()
        {
            return View();
        }


        public ActionResult AddImagesBlock()
        {
            return View();
        }

    }
}
