using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Controllers
{
    public class ArticlesController : Controller
    {
        //
        // GET: /Articles/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new ArticlesViewModel(context);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.Menu;
                return View(model);
            }
        }

    }
}
