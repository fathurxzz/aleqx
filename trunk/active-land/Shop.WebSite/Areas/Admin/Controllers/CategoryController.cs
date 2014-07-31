using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.EntityFramework;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {

        private readonly IShopRepository _repository;

        public CategoryController(IShopRepository repository):base(repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var categories = _repository.GetCategories();
            return View(categories);
        }

        public ActionResult Create(int? id)
        {
            _repository.LangId = CurrentLangId;
            return View(new Category { CategoryId = id, CurrentLang = CurrentLangId });
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            _repository.LangId = CurrentLangId;

            try
            {
                model.Id = 0;
                Category parent = null;
                int categoryLevel = 0;
                if (model.CategoryId != null)
                {
                    parent = _repository.GetCategory((int)model.CategoryId);
                    categoryLevel = parent.CategoryLevel + 1;
                }

                var category = new Category
                {
                    Name = SiteHelper.UpdatePageWebName(model.Name),
                    SortOrder = model.SortOrder,
                    Parent = parent,
                    CategoryLevel = categoryLevel,
                    
                    Title = model.Title,
                    SeoDescription = model.SeoDescription,
                    SeoKeywords = model.SeoKeywords,
                    SeoText = model.SeoText
                };

                _repository.AddCategory(category);
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
                var category = _repository.GetCategory(id);
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(new Category());
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                var category = _repository.GetCategory(model.Id);
                category.Name = SiteHelper.UpdatePageWebName(model.Name);
                TryUpdateModel(category, new[] { "SortOrder", "CategoryLevel", "Title", "SeoDescription", "SeoKeywords", "SeoText" });
                _repository.SaveCategory(category);
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
            try
            {
                _repository.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Attributes(int id)
        {
            var category = _repository.GetCategory(id);
            ViewBag.CategoryId = id;
            var productAttributes = _repository.GetProductAttributes();
            foreach (var productAttribute in productAttributes)
            {
                if (category.ProductAttributes.Contains(productAttribute))
                {
                    productAttribute.Selected = true;
                }
            }
            return View(productAttributes);
        }


        [HttpPost]
        public ActionResult Attributes(int categoryId, FormCollection form)
        {
            _repository.LangId = CurrentLangId;
            var category = _repository.GetCategory(categoryId);
            var attributes = _repository.GetProductAttributes().ToList();
            PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr", "categoryId");

            foreach (var kvp in postData)
            {
                var attribute = attributes.First(a => a.Id == kvp.Key);
                if (kvp.Value)
                {
                    if (!category.ProductAttributes.Contains(attribute))
                        category.ProductAttributes.Add(attribute);
                }
                else
                {
                    if (category.ProductAttributes.Contains(attribute))
                        category.ProductAttributes.Remove(attribute);
                }
            }

            _repository.SaveCategory(category);


            return RedirectToAction("Index");
        }
    }
}
