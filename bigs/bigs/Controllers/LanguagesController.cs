using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;

namespace bigs.Controllers
{
    public class LanguagesController : Controller
    {
        //
        // GET: /Languages/

        public ActionResult SetLanguage(string language, string contentController, string contentUrl)
        {
            SystemSettings.CurrentLanguage = language;
            using (DataStorage context = new DataStorage())
            {
                string contentName = context.SiteContent.Where(sc => sc.Url == contentUrl).Select(sc => sc.Name).First();
                string newUrl = context.SiteContent.Where(sc => sc.Name == contentName && sc.Language == language).Select(sc => sc.Name).First();
                return RedirectToAction("Index", contentController, new { contentUrl = newUrl });
            }
        }
    }
}
