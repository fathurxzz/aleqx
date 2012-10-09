using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new CatalogueContainer())
            {
                var brands = context.Brand.ToList();
                return View(brands);
            }
        }

        public ActionResult Create()
        {
            return View(new Brand());
        } 

        //
        // POST: /Admin/Brand/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var brand = new Brand();
                    TryUpdateModel(brand, new[] { "Title"});
                    context.AddToBrand(brand);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var brand = context.Brand.First(l => l.Id == id);
                return View(brand);
            }
        }

 
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var brand = context.Brand.First(l => l.Id == id);
                    TryUpdateModel(brand, new[] { "Title" });
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var brand = context.Brand.First(l => l.Id == id);
                    context.DeleteObject(brand);
                    context.SaveChanges();
                }
            }
            catch{}
            return RedirectToAction("Index");
        }
    }
}
