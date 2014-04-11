using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2014.Models;
using SiteExtensions;

namespace EM2014.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index(string category, string product)
        {
            var model = new SiteModel(_context, category ?? "", product);
            this.SetSeoContent(model);
            ViewBag.isHomePage = model.Content.IsHomepage;
            ViewBag.Title = model.Title;

            if (model.Product != null)
                return View("Product", model);
            return View(model);
        }


        //public ActionResult PartialContent(string category, string product)
        //{
        //    var model = new SiteModel(_context, category ?? "", product);
        //    this.SetSeoContent(model);
        //    ViewBag.isHomePage = model.Content.IsHomepage;
        //    return PartialView("_IndexPartial", model.Content.Products);
        //}
    }
}
