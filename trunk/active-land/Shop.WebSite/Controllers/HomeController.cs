using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Models;

namespace Shop.WebSite.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly IShopRepository _repository;

        public HomeController(IShopRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var model = new SiteModel(_repository);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Catalogue(string category, string subcategory)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, category, subcategory);
            return View(model);
        }

        public ActionResult ProductDetails(string product)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, productName: product);
            return View(model);
        }
    }
}
