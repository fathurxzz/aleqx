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
            return View(new Content());
        }

        public ActionResult Content(string id, int contentType)
        {
            using (var context = new ContentStorage())
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var content = context.Content.Include("Parent").Where(c => c.Name == id).First();
                    return View("Details", content);
                }

                var mainContent = context.Content.Include("Children").Where(c => c.ContentType == contentType&&c.ContentLevel==0).First();
                return View("Index", mainContent);
            }
        }
    }
}
