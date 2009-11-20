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
            if (contentUrl != null)
            {
                ViewData["contentUrl"] = contentUrl;
                SiteContent content = Utils.GetText(contentUrl);
                ViewData["text"] = content.Text;
            }
            return View();
        }

    }
}
