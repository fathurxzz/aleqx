using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zelenuk.Models;

namespace Zelenuk.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult AddContent()
        {
            return View(new Content());
        }


        [HttpPost]
        public ActionResult AddContent(Content model, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                model.Text = HttpUtility.HtmlDecode(form["Text"]);

                context.AddToContent(model);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = model.Name, area = "" });
            }
        }

        public ActionResult EditContent(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult EditContent(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                TryUpdateModel(content, new[]
                                            {
                                                "Name",
                                                "Title",
                                                "PageTitle",
                                                "SortOrder",
                                                "SeoDescription",
                                                "SeoKeywords"
                                            });

                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { id = content.Name, area = "" });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();
                context.DeleteObject(content);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new {id="", area = "" });
        }

    }
}
