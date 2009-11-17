using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace bigs.Controllers
{
    public class PagePartsController : Controller
    {
        //
        // GET: /PageParts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainMenu()
        {
            return View();
        }

        public ActionResult LanguageBar()
        {
            return View();
        }

    }
}
