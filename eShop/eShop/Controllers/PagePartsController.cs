using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using eShop.Models;

namespace eShop.Controllers
{
    public class PagePartsController : Controller
    {
        //
        // GET: /PageParts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainMenu()
        {
            return View();
        }

        public ActionResult CategoriesList()
        {
            using (ShopStorage context = new ShopStorage())
            {
                List<Category> mainCategories = (from category in context.Categories.Include("Parent") where category.Enabled == true select category).ToList();
                List<SelectListItem> categoriesList =
                    (from category in mainCategories
                     where category.Parent == null
                     select new SelectListItem
                         {
                             Text = category.Name,
                             Value = category.Id.ToString(),
                             Selected = category.Id == SystemSettings.ParentCategoryId
                         }).ToList();
                ViewData["categoriesList"] = categoriesList;

                List<SelectListItem> subCategoriesList = 
                    (from category in mainCategories where category.Parent!=null && category.Parent.Id == SystemSettings.ParentCategoryId
                     select new SelectListItem 
                     { 
                         Text = category.Name, 
                         Value = category.Id.ToString(),
                         Selected = category.Id == SystemSettings.CategoryId 
                     }).ToList();
                if (subCategoriesList.Count == 0)
                    subCategoriesList.Add(new SelectListItem { Text = "----", Value = int.MinValue.ToString() });
                ViewData["subCategoriesList"] = subCategoriesList;

                return View();
            }
        }
    }
}
