using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Models;
using Mayka.Models.Entities;

namespace Mayka.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            using (var context = new SiteContext())
            {
                var content = context.Content.First(c => c.Id == model.Id);

                TryUpdateModel(content, new[]
                {
                    "Title",
                    "Name",
                    "MenuTitle",
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "ContentType"
                });

                content.Text = HttpUtility.HtmlDecode(model.Text);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }

    }
}
