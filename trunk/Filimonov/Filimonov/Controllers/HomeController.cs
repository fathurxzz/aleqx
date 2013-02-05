using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;

namespace Filimonov.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var contents = context.Content.ToList();
                return View(contents);
            }
        }
    }
}
