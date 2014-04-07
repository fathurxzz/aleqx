using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2014.Models;

namespace EM2014.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var content = context.Contents.First(c => c.Id == id);
                return View(content);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            using (var context = new SiteContext())
            {
                var content = context.Contents.First(c => c.Id == model.Id);
                TryUpdateModel(content, new[] { "Name", "Title", "SortOrder", "SeoDescription", "SeoKeywords" });

                context.SaveChanges();

                return RedirectToAction("Index", "Home", new {area = "", category = content.Name});
            }
        }

    }
}
