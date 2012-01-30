using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zelenuk.Helpers;
using Zelenuk.Models;

namespace Zelenuk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                if (id == null)
                    id = "Home";
                var content = context.Content.Where(c => c.Name == id).FirstOrDefault();
                ViewBag.Menu = Menu.GetMenuFromContext(context, id);

                if (content == null)
                {
                    throw new HttpNotFoundException();
                }

                ViewBag.PageTitle = content.PageTitle;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;

                return View(content);
            }
        }

      

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
