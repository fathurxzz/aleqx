using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;

namespace Leo.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        public ActionResult Create()
        {
            using (var context = new SiteContext())
            {
                int maxSortOrder = context.Categories.Max(c => (int?)c.SortOrder) ?? 0;
                var category = new Category
                {
                    SortOrder = maxSortOrder + 1,
                    CurrentLang = CurrentLang.Id
                };
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            using (var context = new SiteContext())
            {
                var cache = new Category
                {
                    Name = SiteHelper.UpdatePageWebName(model.Name),
                    SortOrder = model.SortOrder
                };

                context.Categories.Add(cache);

                var lang = context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(context, model, cache, lang);
                }

                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContext())
            {
                var category = context.Categories.First(c => c.Id == id);
                category.CurrentLang = CurrentLang.Id;
                return View(category);
            }
        }


        [HttpPost]
        public ActionResult Edit(Category model)
        {

            using (var context = new SiteContext())
            {
                var cache = context.Categories.FirstOrDefault(p => p.Id == model.Id);

                if (cache != null)
                {
                    TryUpdateModel(cache, new[] { "SortOrder" });
                    cache.Name = SiteHelper.UpdatePageWebName(model.Name);

                    var lang = context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                    if (lang != null)
                    {
                        CreateOrChangeContentLang(context, model, cache, lang);
                    }
                }
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }


        private void CreateOrChangeContentLang(SiteContext context, Category instance, Category cache, Language lang)
        {

            CategoryLang categoryLang = null;
            if (cache != null)
            {
                categoryLang = context.CategoryLangs.FirstOrDefault(p => p.CategoryId == cache.Id && p.LanguageId == lang.Id);
            }
            if (categoryLang == null)
            {
                var newPostLang = new CategoryLang
                {
                    CategoryId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Text = instance.Text
                };
                context.CategoryLangs.Add(newPostLang);
            }
            else
            {
                categoryLang.Title = instance.Title;
                categoryLang.Text = instance.Text;
            }
            context.SaveChanges();

        }



    }
}
