using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class ProductAttributeValueController : AdminController
    {
        private readonly IShopRepository _repository;

        public ProductAttributeValueController(IShopRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int id)
        {
            _repository.LangId = CurrentLangId;
            var productAttribute = _repository.GetProductAttribute(id);
            var productAttributeValues = productAttribute.ProductAttributeValues;
            ViewBag.ProductAttributeId = id;
            ViewBag.ProductAttributeTitle = productAttribute.Title;
            return View(productAttributeValues);   
        }

        public ActionResult Create(int id)
        {
            _repository.LangId = CurrentLangId;
            return View(new ProductAttributeValue {ProductAttributeId = id, CurrentLang = CurrentLangId});
        }

        [HttpPost]
        public ActionResult Create(ProductAttributeValue model)
        {
            _repository.LangId = CurrentLangId;

            var productAttribute = _repository.GetProductAttribute(model.ProductAttributeId);
            model.ProductAttribute = productAttribute;
            _repository.AddProductAttributeValue(model);

            return RedirectToAction("Index", new {id = productAttribute.Id});
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var pav = _repository.GetProductAttributeValue(id);
                return View(pav);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProductAttributeValue model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var productAttributeValue = _repository.GetProductAttributeValue(model.Id);
                TryUpdateModel(productAttributeValue, new[] { "Title"});
                _repository.SaveProductAttributeValue(productAttributeValue);
                return RedirectToAction("Index", new { id = productAttributeValue .ProductAttributeId});
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {

            return RedirectToAction("Index");
        }

    }
}
