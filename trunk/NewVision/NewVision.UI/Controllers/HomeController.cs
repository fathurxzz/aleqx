using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Models;

namespace NewVision.UI.Controllers
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
            //return RedirectToAction("Index", "MainBanner", new {area = "Admin"});

            ViewBag.Title = "New Vision Pro";

            var content = _context.MainBanners.ToList();



            return View(content);
        }

    }
}
