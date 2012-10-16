using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        public ActionResult Create(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var category = context.Category.Include("CategoryAttributes").First(c => c.Id == id);
                ViewBag.CategoryAttributes = category.CategoryAttributes.ToList();
                ViewBag.CategoryId = id;
                return View(new Brand{Category = category});
            }
        }

        //
        // POST: /Admin/Brand/Create

        [HttpPost]
        public ActionResult Create(int id, FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    var brand = new Brand { Category = category };
                    PostCheckboxesData cbData = form.ProcessPostCheckboxesData("attr");
                    foreach (var kvp in cbData)
                    {
                        var attrId = kvp.Key;
                        bool attrValue = kvp.Value;
                        if (attrValue)
                        {
                            var attribute = context.CategoryAttribute.First(c => c.Id == attrId);
                            brand.CategoryAttributes.Add(attribute);
                        }
                    }

                    TryUpdateModel(brand, new[] { "Title", "Name", "SortOrder", "Href", "DescriptionTitle" });
                    brand.Description = HttpUtility.HtmlDecode(form["Description"]);
                    context.AddToBrand(brand);
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
                var brand = context.Brand.Include("Category").Include("CategoryAttributes").First(l => l.Id == id);
                var category = context.Category.Include("CategoryAttributes").First(c => c.Id == brand.CategoryId);
                ViewBag.CategoryAttributes = category.CategoryAttributes.ToList();
                ViewBag.SelectedAttributes = brand.CategoryAttributes.ToList();

                return View(brand);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new CatalogueContainer())
            {
                var brand = context.Brand.Include("Category").First(b => b.Id == id);
                //var category = context.Category.Include("CategoryAttributes").First(c => c.Id == brand.CategoryId);


                brand.CategoryAttributes.Clear();
                PostCheckboxesData cbData = form.ProcessPostCheckboxesData("attr");
                foreach (var kvp in cbData)
                {
                    var attrId = kvp.Key;
                    bool attrValue = kvp.Value;
                    if (attrValue)
                    {
                        var attribute = context.CategoryAttribute.First(c => c.Id == attrId);
                        brand.CategoryAttributes.Add(attribute);
                    }
                }

                string categoryName = brand.Category.Name;
                TryUpdateModel(brand, new[] { "Title", "Name", "SortOrder", "Href", "DescriptionTitle" });
                brand.Description = HttpUtility.HtmlDecode(form["Description"]);
                context.SaveChanges();


                return RedirectToAction("Index", "Catalogue", new { area = "", category = categoryName });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var brand = context.Brand.Include("Category").First(b => b.Id == id);
                string categoryName = brand.Category.Name;

                brand.CategoryAttributes.Clear();
                while (brand.Products.Any())
                {
                    var product = brand.Products.First();
                    ImageHelper.DeleteImage(product.ImageSource);
                    context.DeleteObject(product);
                }


                context.DeleteObject(brand);
                context.SaveChanges();
                return RedirectToAction("Index", "Catalogue", new { area = "", category = categoryName });
            }
        }
    }
}
