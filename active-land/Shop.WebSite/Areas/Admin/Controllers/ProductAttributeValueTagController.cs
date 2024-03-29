﻿using System;
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

        public ProductAttributeValueTagController(IShopRepository repository) : base(repository)
        {
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

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            var productAttributeValueTag = _repository.GetProductAttributeValueTag(id);
            return View(productAttributeValueTag);
        }

        [HttpPost]
        public ActionResult Edit(ProductAttributeValueTag model)
        {
            _repository.LangId = CurrentLangId;
            var productAttributeValueTag = _repository.GetProductAttributeValueTag(model.Id);
            TryUpdateModel(productAttributeValueTag, new[] { "Title" ,"Name"});
            _repository.SaveProductAttributeValueTag(productAttributeValueTag);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _repository.DeleteProductAttributeValueTag(id);
            return RedirectToAction("Index");
        }

    }
}
