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
                                       "SeoKeywords"
                                   });

                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.SeoText = HttpUtility.HtmlDecode(form["SeoText"]);
                context.AddToContent(content);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "", id = content.Name });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                TryUpdateModel(content,
                              new[]
                                   {
                                       "Title",
                                       "PageTitle",
                                       "SortOrder",
                                       "MainPage",
                                       "SeoTitle",
                                       "SeoDescription",
                                       "SeoKeywords"
                                   });
                
                if (!content.Static)
                {
                    content.Name = form["Name"];
                }

                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                content.SeoText = HttpUtility.HtmlDecode(form["SeoText"]);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "", id = content.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
