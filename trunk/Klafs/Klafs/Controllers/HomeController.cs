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
                var contentItems = context.Content.Where(c => c.Visible).ToList();
                ViewData["contentItems"] = contentItems;
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
