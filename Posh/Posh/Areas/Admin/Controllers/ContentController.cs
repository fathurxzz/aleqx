using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Create()
        {
            return View(new Content());
        }


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var content = new Content();
                TryUpdateModel(content,
                               new[]
                                   {
                                       "Name",
                                       "Title",
                                       "PageTitle",
                                       "SortOrder",
                                       "MainPage",
                                       "SeoTitle",
                                       "SeoDescription",
                                       "SeoKeywords",
                                       "Static"
                                   });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.Text = HttpUtility.HtmlDecode(form["SeoText"]);
                context.AddToContent(content);
                context.SaveChanges();
                return RedirectToAction("Index","Home");
            }
        }
    }
}
