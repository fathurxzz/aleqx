using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.IsHomePage = true;
            return View();
        }

    }
}
