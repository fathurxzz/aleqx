using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filimonov.Areas.Presentation.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Presentation/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
