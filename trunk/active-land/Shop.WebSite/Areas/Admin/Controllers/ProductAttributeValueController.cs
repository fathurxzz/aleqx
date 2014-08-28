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

        public ProductAttributeValueController(IShopRepository repository)
            : base(repository)
        {
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
            var tags = _repository.GetProductAttributeValueTags();
            ViewBag.Tags = tags;
            return View(new ProductAttributeValue { ProductAttributeId = id, CurrentLang = CurrentLangId });
        }

        [HttpPost]
        public ActionResult Create(ProductAttributeValue model, FormCollection form)
        {

            int tagId = int.Parse(form["tag"]);
            var tag = _repository.GetProductAttributeValueTag(tagId);

            _repository.LangId = CurrentLangId;
            var productAttributeValue = new ProductAttributeValue()
            {
                CurrentLang = model.CurrentLang,
                ProductAttributeId = model.ProductAttributeId,
                Title = model.Title,
                ProductAttributeValueTag = tag
            };
            try
            {
                _repository.AddProductAttributeValue(productAttributeValue);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index", new { id = productAttributeValue.ProductAttributeId });
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var pav = _repository.GetProductAttributeValue(id);
                var tags = _repository.GetProductAttributeValueTags();
                ViewBag.Tags = tags;
                return View(pav);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProductAttributeValue model, FormCollection form)
        {
            int tagId = int.Parse(form["tag"]);
            var tag = _repository.GetProductAttributeValueTag(tagId);

            _repository.LangId = CurrentLangId;
            try
            {
                var productAttributeValue = _repository.GetProductAttributeValue(model.Id);
                TryUpdateModel(productAttributeValue, new[] { "Title" });
                productAttributeValue.ProductAttributeValueTag = tag;
                _repository.SaveProductAttributeValue(productAttributeValue);
                return RedirectToAction("Index", new { id = productAttributeValue.ProductAttributeId });
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult Delete(int id, int productAttributeId)
        {
            _repository.DeleteProductAttributeValue(id);
            return RedirectToAction("Index", new { id = productAttributeId });
        }

    }
}
