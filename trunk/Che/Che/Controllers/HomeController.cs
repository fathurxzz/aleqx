using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Che.Models;

namespace Che.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => string.IsNullOrEmpty(id) ? c.ContentType == 0 && c.ContentLevel == 0 : c.Name == id).First();
                ViewData["contentType"] = content.ContentType;
                ViewData["seoDescription"] = content.SeoDescription;
                ViewData["seoKeywords"] = content.SeoKeywords;
                return View(content);
            }
        }

        public ActionResult Content(string id, int contentType)
        {
            using (var context = new ContentStorage())
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var content = context.Content.Include("Parent").Include("Children").Where(c => c.Name == id).First();
                    ViewData["contentType"] = content.ContentType;
                    ViewData["seoDescription"] = content.SeoDescription;
                    ViewData["seoKeywords"] = content.SeoKeywords;
                    return View("Details", content);
                }

                var mainContent = context.Content.Include("Children").Where(c => c.ContentType == contentType && c.ContentLevel == 0).First();
                ViewData["contentType"] = mainContent.ContentType;
                ViewData["seoDescription"] = mainContent.SeoDescription;
                ViewData["seoKeywords"] = mainContent.SeoKeywords;
                return View("Index", mainContent);
            }
        }
    }
}
