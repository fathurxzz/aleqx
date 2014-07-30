using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductAttributeController : AdminController
    {
        private readonly IShopRepository _repository;


        public ProductAttributeController(IShopRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var productAttributes = _repository.GetProductAttributes();
            return View(productAttributes);
        }

        public ActionResult Create()
        {
            _repository.LangId = CurrentLangId;
            return View(new ProductAttribute(){CurrentLang = CurrentLangId});
        }

        

    }
}
