using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Helpers;
using Listelli.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class CategoryController : AdminController
    {
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.Category.Max(c => (int?)c.SortOrder) ?? 0;
                var category = new Category
                                   {
                                       SortOrder = maxSortOrder + 1,
                                       CurrentLang = CurrentLang.Id
                                   };
                return View(category);
            }
        } 

        [HttpPost]
        public ActionResult Create(Category model, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = new Category
                    {
                        Name = SiteHelper.UpdatePageWebName(model.Name),
                        SortOrder = model.SortOrder
                    };

                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                        cache.ImageSource = fileName;
                    }

                    context.AddToCategory(cache);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("Index", "Category", new { area = "FactoryCatalogue" });
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
                var category = context.Category.First(c => c.Id == id);
                category.CurrentLang = CurrentLang.Id;
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Category model, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = context.Category.FirstOrDefault(p => p.Id == model.Id);


                    if (cache != null)
                    {
                        if (fileUpload != null)
                        {
                            if (!string.IsNullOrEmpty(cache.ImageSource))
                            {
                                ImageHelper.DeleteImage(cache.ImageSource);
                            }

                            string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                            string filePath = Server.MapPath("~/Content/Images");
                            filePath = Path.Combine(filePath, fileName);
                            GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                            //fileUpload.SaveAs(filePath);
                            cache.ImageSource = fileName;
                        }

                        TryUpdateModel(cache, new[] { "SortOrder" });
                        cache.Name = SiteHelper.UpdatePageWebName(model.Name);
                        var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                        if (lang != null)
                        {
                            CreateOrChangeContentLang(context, model, cache, lang);
                        }
                    }
                }

                return RedirectToAction("Index", "Category", new { area = "FactoryCatalogue" });
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
                var category = context.Category.Include("CategoryLangs").First(b => b.Id == id);
                ImageHelper.DeleteImage(category.ImageSource);

                while (category.CategoryLangs.Any())
                {
                    var cl = category.CategoryLangs.First();
                    context.DeleteObject(cl);
                }

                context.DeleteObject(category);
                context.SaveChanges();

                return RedirectToAction("Index", "Category", new {area = "FactoryCatalogue"});
            }
        }




        private void CreateOrChangeContentLang(SiteContainer context, Category instance, Category cache, Language lang)
        {

            CategoryLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.CategoryLang.FirstOrDefault(p => p.CategoryId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new CategoryLang
                {
                    CategoryId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                };
                context.AddToCategoryLang(newPostLang);
            }
            else
            {
                contenttLang.Title = instance.Title;
            }
            context.SaveChanges();
        }
    }
}
