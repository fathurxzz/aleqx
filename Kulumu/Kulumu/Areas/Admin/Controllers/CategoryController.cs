using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;

namespace Kulumu.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(FormCollection collection)
        {
            using (var context = new SiteContainer())
            {
                var category = new Category();
                TryUpdateModel(category, new[]
                                                 {
                                                     "Title", 
                                                     "Description", 
                                                     "BottomDescription", 
                                                     "BottomDescriptionTitle"
                                                 });
                context.AddToCategory(category);
                context.SaveChanges();
                return RedirectToAction("Gallery", "Home", new { area = "", id = category.Id });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var category = context.Category.First(c => c.Id == id);
                return View(category);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var category = context.Category.First(c => c.Id == id);
                    TryUpdateModel(category, new[]
                                                 {
                                                     "Title", 
                                                     "Description", 
                                                     "BottomDescription", 
                                                     "BottomDescriptionTitle"
                                                 });
                    context.SaveChanges();
                    return RedirectToAction("Gallery", "Home", new { area = "", id = category.Id });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
