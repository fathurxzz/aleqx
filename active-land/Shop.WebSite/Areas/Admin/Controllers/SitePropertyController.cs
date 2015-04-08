using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class SitePropertyController : AdminController
    {
        //
        // GET: /Admin/SiteProperty/

        public SitePropertyController(IShopRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/SiteProperty/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/SiteProperty/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/SiteProperty/Create

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
        // GET: /Admin/SiteProperty/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/SiteProperty/Edit/5

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
        // GET: /Admin/SiteProperty/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/SiteProperty/Delete/5

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
