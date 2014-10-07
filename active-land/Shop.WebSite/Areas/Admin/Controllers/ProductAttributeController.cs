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


        public ProductAttributeController(IShopRepository repository) : base(repository)
        {
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
            return View(new ProductAttribute(){CurrentLang = CurrentLangId,SortOrder = 0});
        }

        [HttpPost]
        public ActionResult Create(ProductAttribute model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                model.Id = 0;
                var productAttibute = new ProductAttribute()
                {
                    Title = model.Title,
                    UnitTitle = model.UnitTitle,
                    IsStatic = model.IsStatic,
                    IsPublic = model.IsPublic,
                    DisplayOnPreview = model.DisplayOnPreview,
                    IsFilterable = model.IsFilterable,
                    SortOrder = model.SortOrder
                };
                _repository.AddProductAttribute(productAttibute);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var productAttribute = _repository.GetProductAttribute(id);
                return View(productAttribute);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(new ProductAttribute());
        }

        [HttpPost]
        public ActionResult Edit(ProductAttribute model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var productAttribute = _repository.GetProductAttribute(model.Id);
                TryUpdateModel(productAttribute, new[] { "Title", "UnitTitle", "IsStatic", "IsPublic", "DisplayOnPreview", "IsFilterable", "SortOrder" });
                _repository.SaveProductAttribute(productAttribute);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _repository.DeleteProductAttribute(id);
            return RedirectToAction("Index");
        }

    }
}
