using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

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
        public ActionResult EditButtons(string controllerName)
        {
            /*ViewData["controllerName"] = controllerName;
            ViewData["text"] = Utils.GetText(contentUrl).Text;
            ViewData["contentUrl"] = contentUrl;*/
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditButtons(FormCollection form, string controllerName)
        {
            /*ViewData["controllerName"] = controllerName;
            ViewData["text"] = Utils.GetText(contentUrl).Text;
            ViewData["contentUrl"] = contentUrl;*/
            return View();
        }
    }
}
