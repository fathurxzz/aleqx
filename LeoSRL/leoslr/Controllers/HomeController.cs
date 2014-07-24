using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;
using SiteExtensions;

namespace Leo.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Intro()
        {
            var model = new CategoryModel(CurrentLang, _context, intro: true);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Index(string category, string subcategory, string product, int? artId)
        {
            var model = new CategoryModel(CurrentLang, _context, category, subcategory, product, artId);

            ViewBag.Language = CurrentLang.Code;
            ViewBag.Category = category;
            ViewBag.SubCategory = subcategory;

            this.SetSeoContent(model);
            return View(model);
        }


    }
}
