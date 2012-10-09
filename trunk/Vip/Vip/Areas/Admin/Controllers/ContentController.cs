using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Edit(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var content = context.Content.First(c => c.Id == id);
                    TryUpdateModel(content, new[] {"Title","DescriptionTitle","SeoDescription","SeoKeywords","SortOrder"});
                    content.Text = HttpUtility.HtmlDecode(form["Text"]);
                    content.Description = HttpUtility.HtmlDecode(form["Description"]);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home", new {area="", id = content.Name});
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
