using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Helpers.Graphics;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {


        public CategoryController(IShopRepository repository):base(repository)
        {
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
            return View(new Category { CategoryId = id, CurrentLang = CurrentLangId, SortOrder = 0});
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
                    IsActive = model.IsActive,
                    Title = model.Title,
                    SeoDescription = model.SeoDescription,
                    SeoKeywords = model.SeoKeywords,
                    SeoText = model.SeoText
                };

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    category.ImageSource = fileName;
                }
                else
                {
                    category.ImageSource = category.ImageSource ?? "";
                }


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
                TryUpdateModel(category, new[] { "SortOrder","IsActive", "CategoryLevel", "Title", "SeoDescription", "SeoKeywords", "SeoText" });
                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    if (!string.IsNullOrEmpty(category.ImageSource))
                    {
                        ImageHelper.DeleteImage(category.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    category.ImageSource = fileName;
                }
                else
                {
                    category.ImageSource = category.ImageSource ?? "";
                }

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
                _repository.DeleteCategory(id, ImageHelper.DeleteImage);
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
