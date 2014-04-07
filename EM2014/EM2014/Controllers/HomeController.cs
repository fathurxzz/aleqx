using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2014.Models;

namespace EM2014.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Title = "EM2014";

            using (var context = new SiteContext())
            {
                var content = context.Contents.FirstOrDefault();
            }

            return View();
        }

    }
}
