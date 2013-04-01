using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class HomeController : Controller
    {
        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult Index()
        {
            ViewBag.IsHomePage = true;
            return View();
        }

    }
}
