using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class CategoryBrandItemController : AdminController
    {

        public ActionResult Create(int brandId)
        {
            using (var context = new SiteContainer())
            {
                var brand = context.CategoryBrand.Include("Category").First(b => b.Id == brandId);
                int maxSortOrder = context.CategoryBrandItem.Where(c => c.CategoryBrandId == brandId).Max(c => (int?)c.SortOrder) ?? 0;
                var brandItem = new CategoryBrandItem
                                    {
                                        CategoryBrand = brand,
                                        SortOrder = maxSortOrder + 1,
                                        CurrentLang = CurrentLang.Id
                                    };
                ViewBag.BrandName = brand.Name;
                ViewBag.CategoryName = brand.Category.Name;
                return View(brandItem);
            }
        } 

        [HttpPost]
        public ActionResult Create(CategoryBrandItem model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var brand = context.CategoryBrand.Include("Category").First(b => b.Id == model.CategoryBrandId);

                    var cache = new CategoryBrandItem
                    {
                        CategoryBrand = brand,
                        Content = model.Content,
                        SortOrder = model.SortOrder
                    };

                    context.AddToCategoryBrandItem(cache);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("Details", "Brand",
                                            new
                                                {
                                                    area = "FactoryCatalogue",
                                                    categoryId = brand.Category.Name,
                                                    id = brand.Name
                                                });
                }
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.CategoryBrandItem.Include("CategoryBrand").First(b => b.Id == id);
                var categoryBrand = context.CategoryBrand.Include("Category").First(c => c.Id == brandItem.CategoryBrandId);

                brandItem.CurrentLang = CurrentLang.Id;

                ViewBag.BrandName = brandItem.CategoryBrand.Name;
                ViewBag.CategoryName = categoryBrand.Category.Name;

                return View(brandItem);
            }
        }

        [HttpPost]
        public ActionResult Edit(CategoryBrandItem model)
        {
            try
            {
                
                using (var context = new SiteContainer())
                {

                    var brandItem = context.CategoryBrandItem.Include("CategoryBrand").First(b => b.Id == model.Id);
                    var categoryBrand = context.CategoryBrand.Include("Category").First(c => c.Id == brandItem.CategoryBrandId);


                    TryUpdateModel(brandItem, new[] { "Content", "SortOrder" });

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, brandItem, lang);
                    }

                    return RedirectToAction("Details", "Brand",
                                             new
                                             {
                                                 area = "FactoryCatalogue",
                                                 categoryId = categoryBrand.Category.Name,
                                                 id = brandItem.CategoryBrand.Name
                                             });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var brandItem = context.CategoryBrandItem.Include("CategoryBrandItemLangs").Include("CategoryBrand").First(b => b.Id == id);
                var categoryBrand = context.CategoryBrand.Include("Category").First(c => c.Id == brandItem.CategoryBrandId);

                while (brandItem.CategoryBrandItemLangs.Any())
                {
                    var bl = brandItem.CategoryBrandItemLangs.First();
                    context.DeleteObject(bl);
                }

                context.DeleteObject(brandItem);
                context.SaveChanges();
                return RedirectToAction("Details", "Brand", new { area = "FactoryCatalogue", categoryId = categoryBrand.Category.Name, id = categoryBrand.Name });
            }
        }


        private void CreateOrChangeContentLang(SiteContainer context, CategoryBrandItem instance, CategoryBrandItem cache, Language lang)
        {

            CategoryBrandItemLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.CategoryBrandItemLang.FirstOrDefault(p => p.CategoryBrandItemId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new CategoryBrandItemLang
                {
                    CategoryBrandItemId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Text = instance.Text
                };
                context.AddToCategoryBrandItemLang(newPostLang);
            }
            else
            {
                contenttLang.Title = instance.Title;
                contenttLang.Text = instance.Text;
            }
            context.SaveChanges();

        }
    }
}
