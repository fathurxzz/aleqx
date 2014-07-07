using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;
using SiteExtensions;

namespace Leo.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly SiteContext _context;

        public CategoryController(SiteContext context)
        {
            _context = context;
        }


        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            foreach (var category in categories)
            {
                category.CurrentLang = CurrentLang.Id;
            }
            return View(categories);
        }


        public ActionResult Create(int? id)
        {
            return View(new Category { CategoryId = id, CurrentLang = CurrentLang.Id });
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            //ModelState.Clear();

            model.Id = 0;
            Category parent = null;
            int categoryLevel = 0;
            if (model.CategoryId != null)
            {
                parent = _context.Categories.First(c => c.Id == model.CategoryId);
                categoryLevel = parent.CategoryLevel + 1;
            }

            var cache = new Category
            {
                Name = SiteHelper.UpdatePageWebName(model.Name),
                SortOrder = model.SortOrder,
                Parent = parent,
                CategoryLevel = categoryLevel,
                //CategoryId = model.CategoryId
            };

            model.Text = model.Text ?? "";

            _context.Categories.Add(cache);
            //_context.SaveChanges();

            var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
            if (lang != null)
            {
                CreateOrChangeContentLang(_context, model, cache, lang);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {

            var category = _context.Categories.First(c => c.Id == id);
            category.CurrentLang = CurrentLang.Id;
            return View(category);

        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {


            var cache = _context.Categories.FirstOrDefault(p => p.Id == model.Id);

            if (cache != null)
            {
                TryUpdateModel(cache, new[] { "SortOrder" });
                cache.Name = SiteHelper.UpdatePageWebName(model.Name);
                model.Text = model.Text ?? "";
                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }
            }


            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var category = _context.Categories.First(p => p.Id == id);
            while (category.CategoryLangs.Any())
            {
                var lang = category.CategoryLangs.First();
                _context.CategoryLangs.Remove(lang);
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
