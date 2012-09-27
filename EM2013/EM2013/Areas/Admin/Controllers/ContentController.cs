using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Models;

namespace EM2013.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (var context = new SiteContext())
            {
                var content = context.Content.First(c => c.Id == id);
                TryUpdateModel(content, new[] {"Title", "Text", "SeoDescription", "SeoKeywords"});
                context.SaveChanges();
                return RedirectToAction("Index","Home",new{area=""});
            }
        }
    }
}
