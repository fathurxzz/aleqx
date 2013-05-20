using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Controllers;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Areas.FactoryCatalogue.Controllers
{
    public class CategoryController : DefaultController
    {
        //
        // GET: /FactoryCatalogue/Category/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new FactoryCatalogueModel(CurrentLang, context, null,null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult Details(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new FactoryCatalogueModel(CurrentLang, context, id,null);
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = "factory-details";
                if (model.Category.CategoryBrands.Any())
                {
                    var minSortOrder = model.Category.CategoryBrands.Min(c => (int?)c.SortOrder) ?? 0;
                    return RedirectToAction("Details", "Brand", new {area = "FactoryCatalogue", categoryId=model.Category.Name,id=model.Category.CategoryBrands.First(c=>c.SortOrder==minSortOrder).Name});
                }
                return View(model);
            }
        }

        //
        // GET: /FactoryCatalogue/Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FactoryCatalogue/Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /FactoryCatalogue/Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FactoryCatalogue/Category/Edit/5

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
        // GET: /FactoryCatalogue/Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FactoryCatalogue/Category/Delete/5

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
    }
}
