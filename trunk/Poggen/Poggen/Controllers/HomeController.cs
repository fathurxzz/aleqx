using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poggen.Models;
using SiteExtensions;

namespace Poggen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteViewModel(context,null);
                this.SetSeoContent(model);
                return View();
            }
        }
    }
}
