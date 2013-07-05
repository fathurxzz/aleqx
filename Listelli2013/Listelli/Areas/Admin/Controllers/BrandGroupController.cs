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
    public class BrandGroupController : AdminController
    {
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.BrandGroup.Max(c => (int?)c.SortOrder) ?? 0;
                var brand = new BrandGroup
                {
                    SortOrder = maxSortOrder + 1,
                    CurrentLang = CurrentLang.Id
                };
                return View(brand);
            }
        }

        [HttpPost]
        public ActionResult Create(BrandGroup model, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = new BrandGroup
                    {
                        Name = SiteHelper.UpdatePageWebName(model.Name),
                        Description = model.Description,
                        SortOrder = model.SortOrder
                    };


                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload, 500);
                        //fileUpload.SaveAs(filePath);
                        cache.ImageSource = fileName;
                    }

                    context.AddToBrandGroup(cache);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("Index", "Home", new { area = "BrandCatalogue" });
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
                var brand = context.BrandGroup.First(c => c.Id == id);
                brand.CurrentLang = CurrentLang.Id;
                return View(brand);
            }
        }


        [HttpPost]
        public ActionResult Edit(BrandGroup model, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = context.BrandGroup.FirstOrDefault(p => p.Id == model.Id);


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


                        TryUpdateModel(cache, new[] { "Description", "SortOrder" });
                        cache.Name = SiteHelper.UpdatePageWebName(model.Name);

                        var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                        if (lang != null)
                        {
                            CreateOrChangeContentLang(context, model, cache, lang);
                        }
                    }
                }

                return RedirectToAction("Index", "Home", new { area = "BrandCatalogue" });
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
                var brand = context.BrandGroup.Include("BrandGroupLangs").First(b => b.Id == id);
                ImageHelper.DeleteImage(brand.ImageSource);

                while (brand.BrandGroupLangs.Any())
                {
                    var bl = brand.BrandGroupLangs.First();
                    context.DeleteObject(bl);
                }
                context.DeleteObject(brand);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "BrandCatalogue" });
            }
        }




        private void CreateOrChangeContentLang(SiteContainer context, BrandGroup instance, BrandGroup cache, Language lang)
        {

            BrandGroupLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.BrandGroupLang.FirstOrDefault(p => p.BrandGroupId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new BrandGroupLang
                {
                    BrandGroupId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Description = instance.Description
                };
                context.AddToBrandGroupLang(newPostLang);
            }
            else
            {
                contenttLang.Title = instance.Title;
                contenttLang.Description = instance.Description;
            }
            context.SaveChanges();

        }
    }
}
