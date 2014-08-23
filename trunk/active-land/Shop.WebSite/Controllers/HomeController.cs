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

        public HomeController(IShopRepository repository) : base(repository)
        {
        }

        public ActionResult Index(string id)
        {
            _repository.LangId = CurrentLangId;
            var model = new SiteModel(_repository, id) { CurrentLangCode = CurrentLangCode };
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Catalogue(string category, string filter, int? page)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, page, category, filter: filter)
            {
                CurrentLangCode = CurrentLangCode
            };
            ViewBag.ProductTotalCount = model.ProductTotalCount;
            ViewBag.Page = model.Page;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult ProductDetails(string product)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, null, productName: product)
            {
                CurrentLangCode = CurrentLangCode
            };
            this.SetSeoContent(model);
            ViewBag.CurrentLangCode = CurrentLangCode;
            return View(model);
        }

        public ActionResult ArticleDetails(string article)
        {
            _repository.LangId = CurrentLangId;
            var model = new CatalogueModel(_repository, null, articleName: article)
            {
                CurrentLangCode = CurrentLangCode
            };
            this.SetSeoContent(model);
            return View(model);
        }
    }
}
