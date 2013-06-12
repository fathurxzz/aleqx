using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ego.Models;

namespace Ego.Areas.Admin.Controllers
{
    [Authorize]
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
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (var context = new SiteContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                TryUpdateModel(content, new[] {"Title","Sortorder","SeoDescription","SeoKeywords","MainPage"});
                content.Text = HttpUtility.HtmlDecode(collection["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area="", id = content.Name });
            }
        }
    }
}
