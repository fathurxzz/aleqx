using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;

namespace bigs.Controllers
{
    public class BaseContentController : Controller
    {
        //
        // GET: /BaseContent/

        public ActionResult Index(string contentUrl)
        {
            /*
            if (!string.IsNullOrEmpty(contentUrl))
            {
                ViewData["contentUrl"] = contentUrl;
                SiteContent content = Utils.GetContent(contentUrl);
                ViewData["text"] = content.Text;
                ViewData["title"] = content.Title;
                ViewData["keywords"] = content.Keywords;
                ViewData["description"] = content.Description;
            }
            */

            return View();
        }
    }
}
