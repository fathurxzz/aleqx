using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;
using SiteExtensions;

namespace Leo.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                var category = new Category { SortOrder = 0 };
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var category = new Category();


                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr");
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

                TryUpdateModel(category, new[] { 
                    "Name", 
                    "Title", 
                    "SortOrder", 
                    "SeoDescription", 
                    "SeoKeywords" });

                context.AddToCategory(category);
                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue", new { Area = "", id = category.Name });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.Include("ProductAttributes").First(c => c.Id == id);
                var attributes = context.ProductAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Category model, FormCollection form)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.First(c => c.Id == model.Id);

                TryUpdateModel(category, new[]{
                    "Name",
                    "Title",
                    "SortOrder",
                    "SeoDescription", 
                    "SeoKeywords"
                });

                var attributes = context.ProductAttribute.ToList();
                PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr");
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

                context.SaveChanges();

                return RedirectToAction("Index", "Catalogue", new { Area = "", id = category.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.Include("Products").First(c => c.Id == id);
                while (category.Products.Any())
                {
                    var product = category.Products.First();
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                        IOHelper.DeleteFile("~/ImageCache/galleryThumbnail", product.ImageSource);
                        product.ProductAttributes.Clear();
                        context.DeleteObject(product);
                    }
                }
                context.SaveChanges();

                category.ProductAttributes.Load();
                category.ProductAttributes.Clear();
                context.DeleteObject(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "" });
        }
    }
}
