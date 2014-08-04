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

        public ActionResult Catelogue(string category, string subcategory, string product)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository);
            return View(model);
        }
    }
}
