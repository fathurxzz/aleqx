using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;
using SiteExtensions;

namespace Penetron.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Technologies(string id)
        {
            var model = new TechnologyModel(_context, id);
            ViewBag.IsHomePage = model.IsHomePage;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult SiteContent(string id)
        {
            var model = new SiteModel(_context, id);
            ViewBag.IsHomePage = model.IsHomePage;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Buildings()
        {
            return View();
        }

    }
}
