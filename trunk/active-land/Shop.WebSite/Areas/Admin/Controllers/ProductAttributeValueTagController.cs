using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductAttributeValueTagController : AdminController
    {
        private readonly IShopRepository _repository;

        public ProductAttributeValueTagController(IShopRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var productAttributeValueTags = _repository.GetProductAttributeValueTags();
            return View(productAttributeValueTags);
        }

        public ActionResult Create()
        {
            _repository.LangId = CurrentLangId;
            return View(new ProductAttributeValueTag {CurrentLang = CurrentLangId});
        }

        [HttpPost]
        public ActionResult Create(ProductAttributeValueTag model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                _repository.AddProductAttributeValueTag(model);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }



    }
}
