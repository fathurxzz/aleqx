using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new CatalogueContainer())
            {
                var categories = context.Category.ToList();
                return View(categories);
            }
        }

        public ActionResult Create()
        {
            using (var context = new CatalogueContainer())
            {
                var category = new Category { SortOrder = 0 };
                var attributes = context.CategoryAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var category = new Category();


                    var attributes = context.CategoryAttribute.ToList();
                    PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in postData)
                    {
                        var attribute = attributes.First(a => a.Id == kvp.Key);
                        if (kvp.Value)
                        {
                            if (!category.CategoryAttributes.Contains(attribute))
                                category.CategoryAttributes.Add(attribute);
                        }
                        else
                        {
                            if (category.CategoryAttributes.Contains(attribute))
                                category.CategoryAttributes.Remove(attribute);
                        }
                    }

                    TryUpdateModel(category, new[] { 
                    "Name", 
                    "Title", 
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "DescriptionTitle"
                    });
                    category.Description = HttpUtility.HtmlDecode(form["Description"]);

                    context.AddToCategory(category);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Catalogue", new { area = "", category = category.Name });
                }


            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var category = context.Category.Include("CategoryAttributes").First(c => c.Id == id);
                var attributes = context.CategoryAttribute.ToList();
                ViewBag.Attributes = attributes;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var category = context.Category.First(c => c.Id == id);

                    TryUpdateModel(category, new[]{
                    "Name",
                    "Title",
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "DescriptionTitle"
                });
                    category.Description = HttpUtility.HtmlDecode(form["Description"]);

                    var attributes = context.CategoryAttribute.ToList();
                    PostCheckboxesData postData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in postData)
                    {
                        var attribute = attributes.First(a => a.Id == kvp.Key);
                        if (kvp.Value)
                        {
                            if (!category.CategoryAttributes.Contains(attribute))
                                category.CategoryAttributes.Add(attribute);
                        }
                        else
                        {
                            if (category.CategoryAttributes.Contains(attribute))
                                category.CategoryAttributes.Remove(attribute);
                        }
                    }

                    context.SaveChanges();
                    return RedirectToAction("Index", "Catalogue", new { area = "", category = category.Name});
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var category = context.Category.Include("Brands").Include("CategoryAttributes").First(c => c.Id == id);
                    if (!category.Brands.Any())
                    {
                        category.CategoryAttributes.Clear();
                        context.DeleteObject(category);
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }
    }
}
