using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    public class CategoryBrandController : AdminController
    {
        
        public ActionResult Create(int categoryId)
        {

            using (var context = new SiteContainer())
            {
                int maxSortOrder = context.CategoryBrand.Where(c=>c.CategoryId==categoryId).Max(c => (int?)c.SortOrder) ?? 0;
                var categoryBrand = new CategoryBrand
                {
                    SortOrder = maxSortOrder + 1
                };
                return View(categoryBrand);
            }
        } 


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/CategoryBrand/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/CategoryBrand/Edit/5

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
        // GET: /Admin/CategoryBrand/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/CategoryBrand/Delete/5

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
