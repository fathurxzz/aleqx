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
            ViewBag.IsHomePage = true;

            return View();
        }


        public ActionResult Technologies(string categoryId, string subCategoryId)
        {
            var model = new TechnologyModel(_context, categoryId,subCategoryId);
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.CategoryLevel = model.Technology.CategoryLevel == 0 ? "technologyRoot" : "technology";
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult SiteContent(string id)
        {
            var model = new SiteModel(_context, id);
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.CurrentPage = model.Content.Name;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Buildings(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId);
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "buildingRoot" : "building";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

    }
}
