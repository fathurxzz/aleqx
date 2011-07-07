using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Che.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {

            return View();
        }

        public ActionResult Content(string id)
        {

            return View("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
