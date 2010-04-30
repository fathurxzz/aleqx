using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ViaCon.Models;

namespace ViaCon.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                ViewData["id"] = id;
                Content content = context.Content.Where(c => c.ContentId == id).FirstOrDefault();
                return View("Content", content);
            }
        }

    }
}
