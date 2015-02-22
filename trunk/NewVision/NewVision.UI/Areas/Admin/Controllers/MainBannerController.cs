using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class MainBannerController : Controller
    {

        private readonly SiteContext _context;

        public MainBannerController(SiteContext context)
        {
            _context = context;
        }
        //
        // GET: /Admin/MainBanner/

        public ActionResult Index()
        {
            var mainBanners = _context.MainBanners.ToList();
            return View(mainBanners);
        }

        //
        // GET: /Admin/MainBanner/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/MainBanner/Create

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
        // GET: /Admin/MainBanner/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/MainBanner/Edit/5

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
        // GET: /Admin/MainBanner/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/MainBanner/Delete/5

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
