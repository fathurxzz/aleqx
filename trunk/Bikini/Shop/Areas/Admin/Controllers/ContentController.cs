using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class ContentController : AdminController
    {
        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                Content content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            using (var context = new ShopContainer())
            {
                Content content = context.Content.First(c => c.Id == model.Id);
                TryUpdateModel(content, new[] {"Name","Title","SortOrder","SeoDescription","SeoKeywords"});
                content.Text = HttpUtility.HtmlDecode(model.Text);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }


        }

    }
}
