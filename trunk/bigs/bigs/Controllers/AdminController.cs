using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;

namespace bigs.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public ActionResult EditText(string contentUrl, string controllerName)
        {
            ViewData["controllerName"] = controllerName;
            ViewData["text"] = Utils.GetText(contentUrl).Text;
            ViewData["contentUrl"] = contentUrl;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditText(string text, string controllerName, string contentUrl)
        {
            Utils.SetText(contentUrl, HttpUtility.HtmlDecode(text));
            return RedirectToAction("Index", controllerName, new { contentUrl = contentUrl });
        }


        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public ActionResult EditTransfers(string contentUrl, string controllerName)
        {
            ViewData["controllerName"] = controllerName;
            ViewData["text"] = Utils.GetText(contentUrl).Text;
            ViewData["contentUrl"] = contentUrl;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditTransfers(string text, string controllerName, string contentUrl)
        {
            Utils.SetText(contentUrl, HttpUtility.HtmlDecode(text));

            using (DataStorage context = new DataStorage())
            {
                string newUrl = context.SiteContent.Where(sc => sc.Name == "Services" && sc.Language == SystemSettings.CurrentLanguage).Select(sc => sc.Url).First();



                return RedirectToAction("Index", controllerName, new { contentUrl = newUrl });
            }
        }
    }
}
