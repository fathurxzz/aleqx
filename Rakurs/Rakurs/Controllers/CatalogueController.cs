using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rakurs.Helpers;
using Rakurs.Models;

namespace Rakurs.Controllers
{
    public class CatalogueController : Controller
    {
        //
        // GET: /Catalogue/

        public ActionResult Index(string category, string subCategory, int? filter)
        {
            using (var context = new StructureContainer())
            {
                var model = new CatalogueViewModel(context, category, subCategory, filter);

                if (model.SubCategory == null && model.Category.Products.Count == 0 && model.Category.Children.Count > 0)
                {
                    var child = model.Category.Children.First();
                    return RedirectToAction("Index", new { category = model.Category.Name, subCategory = child.Name });
                }


                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.CategoryName = model.Category.Name;
                if (model.SubCategory != null)
                    ViewBag.SubCategoryName = model.SubCategory.Name;
                ViewBag.CategoryName = model.Category.Name;
                ViewBag.Filter = model.CurrentFilterId;
                if (model.Content != null)
                    ViewBag.ContentName = model.Content.Name;
                return View(model);
            }
        }

    }
}
