using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Controllers
{
    [HandleError()]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Projects(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model);
            }
        }

        public ActionResult ProjectDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model);
            }
        }
    }
}
