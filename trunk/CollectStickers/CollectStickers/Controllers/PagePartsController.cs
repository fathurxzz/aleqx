using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CollectStickers.Controllers
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
            ViewData["authenticated"] = User.Identity.IsAuthenticated;
            ViewData["isAdmin"] = User.Identity.Name.ToLower() == "admin";
            return View();
        }

    }
}
