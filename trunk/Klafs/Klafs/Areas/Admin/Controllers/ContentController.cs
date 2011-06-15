using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klafs.Models;

namespace Klafs.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult EditContent(int? id)
        {
            using (var context = new ContentStorage())
            {
                Content content;

                if (id == null)
                {
                    content = context.Content.Where(c => c.Id == 1).First();
                }
                else
                {
                    content = context.Content.Where(c => c.Id == id).FirstOrDefault();
                }

                return View(content);
            }
        }


        [HttpPost]
        public ActionResult EditContent(int id, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                Content content = context.Content.Where(c => c.Id == id).FirstOrDefault();

                TryUpdateModel(content, new string[] { "Name", "PageTitle", "Title", "SeoKeywords", "SeoDescription", "Description", "Sign", "Sign2","MenuTitle"});
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.SeoText = HttpUtility.HtmlDecode(form["SeoText"]);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }
        }


    }
}
