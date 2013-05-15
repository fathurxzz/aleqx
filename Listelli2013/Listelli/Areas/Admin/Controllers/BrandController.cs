using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    public class BrandController : AdminController
    {
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
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
        public ActionResult Create(Brand model)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var cache = new Brand
                                    {
                                        Name = model.Name, 
                                        Description = model.Description,
                                        SortOrder = model.SortOrder
                                    };
                    context.AddToBrand(cache);

                    var lang = context.Language.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }

                    return RedirectToAction("Gallery", "Home", new { area = "" });
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Brand/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Brand/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
