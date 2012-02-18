using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models;

namespace Shop.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        //
        // GET: /Admin/Brand/

        public ActionResult Index()
        {
            using (var context = new ShopContainer())
            {
                var brands = context.Brand.ToList();
                return View(brands);
            }
        }

        //
        // GET: /Admin/Brand/Details/5

        public ActionResult Details(int id)
        {
            using (var context = new ShopContainer())
            {
                var brand = context.Brand.Where(b => b.Id == id).First();
                return View(brand);
            }
        }

        //
        // GET: /Admin/Brand/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Brand/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var brand = new Brand { Name = collection["Name"] };
                    context.AddToBrand(brand);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Brand/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new ShopContainer())
            {
                var brand = context.Brand.Where(b => b.Id == id).First();
                return View(brand);
            }
        }

        //
        // POST: /Admin/Brand/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new ShopContainer())
                {
                    var brand = context.Brand.First(b => b.Id == id);
                    TryUpdateModel(brand, new[] {"Name", "Description", "SeoDescription", "SeoKeywords"});
                    context.SaveChanges();
                }

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
            using (var context = new ShopContainer())
            {
                var brand = context.Brand.Where(b => b.Id == id).First();
                context.DeleteObject(brand);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
