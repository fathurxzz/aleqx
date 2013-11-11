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
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Technologies", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = 0;
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
            var model = new BuildingModel(_context, categoryId, subCategoryId,(int)EContentType.Buildings);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Buildings", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int) EContentType.Buildings;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "buildingRoot" : "building";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Products(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.Products);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Products", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.Products;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "productRoot" : "product";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Documents(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.Documents);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("Documents", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.Documents;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "documentRoot" : "document";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult WhereToBuy(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.WhereToBuy);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("WhereToBuy", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.WhereToBuy;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "wheretobuyRoot" : "wheretobuy";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult About(string categoryId, string subCategoryId)
        {
            var model = new BuildingModel(_context, categoryId, subCategoryId, (int)EContentType.About);
            if (model.ActiveCategoryNotFound)
                return RedirectToAction("About", new { categoryId = model.RedirectCategoryId, subCategoryId = model.RedirectSubCategoryId });
            ViewBag.IsHomePage = model.IsHomePage;
            ViewBag.ContentType = (int)EContentType.About;
            ViewBag.CategoryLevel = model.Building.CategoryLevel == 0 ? "aboutRoot" : "about";
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            this.SetSeoContent(model);
            return View(model);
        }



    }
}
