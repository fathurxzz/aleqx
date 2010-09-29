using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Select(c => c).Where(c => c.ContentId == id).First();
                return View("Content", content);
            }
        }
    }
}
