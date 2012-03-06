using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var contents = context.Content.ToList();
                return View(contents);
            }
        }

        public ActionResult Create()
        {
            return View(new Content());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var content = new Content();
                TryUpdateModel(content,
                               new[]
                                   {
                                       "Name",
                                       "Title",
                                       "PageTitle",
                                       "SortOrder",
                                       "Published",
                                       "MainPage",
                                       "SeoText",
                                       "SeoDescription",
                                       "SeoKeywords"
                                   });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.AddToContent(content);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                return View(content);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ShopContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                TryUpdateModel(content,
                              new[]
                                   {
                                       "Name",
                                       "Title",
                                       "PageTitle",
                                       "SortOrder",
                                       "Published",
                                       "MainPage",
                                       "SeoText",
                                       "SeoDescription",
                                       "SeoKeywords"
                                   });
                content.Text = HttpUtility.HtmlDecode(form["Text"]);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ShopContainer())
            {
                var content = context.Content.First(c => c.Id == id);
                context.DeleteObject(content);
                return RedirectToAction("Index");
            }
        }
    }
}
