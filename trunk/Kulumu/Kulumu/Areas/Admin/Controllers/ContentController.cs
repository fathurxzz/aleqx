using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;

namespace Kulumu.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Edit(string id)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Name == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Name == id);
                TryUpdateModel(content, new[] {"Title","DescriptionTitle","SeoDescription","SeoKeywords"});
                content.Description = HttpUtility.HtmlDecode(form["Description"]);
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new {area = "", id = content.Name});
            }
        }
    }
}
