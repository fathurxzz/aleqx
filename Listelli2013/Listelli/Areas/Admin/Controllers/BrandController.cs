﻿using System.IO;
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
    public class BrandController : AdminController
    {
        public ActionResult Create(int brandId)
        {
            using (var context = new SiteContainer())
            {
                var brandGroup = context.BrandGroup.First(b => b.Id == brandId);
                ViewBag.BrandGroupName = brandGroup.Name;
                ViewBag.BrandId = brandId;
                int maxSortOrder = context.Brand.Max(c => (int?)c.SortOrder) ?? 0;
                var brand = new Brand
                                {
                                    SortOrder = maxSortOrder + 1,
                                    CurrentLang = CurrentLang.Id
                                };
                return View(brand);
                
            }
        }

        [HttpPost]
        public ActionResult Create(Brand model, HttpPostedFileBase fileUpload, int brandId)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var brandGroup = context.BrandGroup.First(b => b.Id == brandId);

                    var cache = new Brand
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

                        cache.BrandGroup = brandGroup;

                        context.AddToBrand(cache);

                        var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                        if (lang != null)
                        {
                            CreateOrChangeContentLang(context, model, cache, lang);
                        }
                    }
                    return RedirectToAction("BrandGroupDetails", "Home", new { area = "BrandCatalogue",id=brandGroup.Name });
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
                var brand = context.Brand.Include("BrandGroup").First(c => c.Id == id);
                brand.CurrentLang = CurrentLang.Id;
                return View(brand);
            }
        }

        [HttpPost]
        public ActionResult Edit(Brand model, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = context.Brand.Include("BrandGroup").FirstOrDefault(p => p.Id == model.Id);

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

                    return RedirectToAction("BrandGroupDetails", "Home", new { area = "BrandCatalogue", id = cache.BrandGroup.Name });
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
                var brand = context.Brand.Include("BrandGroup").Include("BrandLangs").First(b => b.Id == id);
                var bgn = brand.BrandGroup.Name;
                ImageHelper.DeleteImage(brand.ImageSource);

                while (brand.BrandLangs.Any())
                {
                    var bl = brand.BrandLangs.First();
                    context.DeleteObject(bl);
                }
                context.DeleteObject(brand);
                context.SaveChanges();

                return RedirectToAction("BrandGroupDetails", "Home", new {area = "BrandCatalogue", id = bgn});
            }
        }

        
        private void CreateOrChangeContentLang(SiteContainer context, Brand instance, Brand cache, Language lang)
        {

            BrandLang contenttLang = null;
            if (cache != null)
            {
                contenttLang = context.BrandLang.FirstOrDefault(p => p.BrandId == cache.Id && p.LanguageId == lang.Id);
            }
            if (contenttLang == null)
            {
                var newPostLang = new BrandLang
                {
                    BrandId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Description = instance.Description
                };
                context.AddToBrandLang(newPostLang);
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
