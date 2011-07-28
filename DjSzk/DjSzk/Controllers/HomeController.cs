using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DjSzk.Models;

namespace DjSzk.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("MusicContent").Where(c => string.IsNullOrEmpty(id) ? c.Id == 1 : c.Name == id).First();

                var menuItems = context.Content.Where(c => c.Id != 1).ToList();
                ViewData["menuItems"] = menuItems;
                ViewData["contentName"] = content.Name;
                ViewData["seoDescription"] = content.SeoDescription;
                ViewData["seoKeywords"] = content.SeoKeywords;

                return View(content);
            }
        }
    }
}
