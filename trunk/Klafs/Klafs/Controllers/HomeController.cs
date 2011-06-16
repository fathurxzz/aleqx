using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klafs.Models;

namespace Klafs.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var contentItems = context.Content.Where(c => c.Visible && c.ContentType == 1).OrderBy(c => c.SortOrder).ToList();
                ViewData["contentItems"] = contentItems;

                var headerMenuItems = context.Content.Where(c => c.Visible && (c.ContentType == 0 || c.ContentType == 3)).OrderBy(c => c.SortOrder).ToList();
                ViewData["headerMenuItems"] = headerMenuItems;



                Content content;
                if (id == null)
                    content = context.Content.Where(c => c.Id == 1).FirstOrDefault();
                else
                {
                    content = context.Content.Include("GalleryItem").Where(c => c.Name == id).FirstOrDefault();
                }

                ViewData["contentType"] = content.ContentType;

                return View(content);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
