using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        private readonly SiteContext _context;

        public ProductController(SiteContext context)
        {
            _context = context;
        }

        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        //
        // GET: /Admin/Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Product/Create

        public ActionResult Create(int? id)
        {
            var authors = _context.Authors.ToList();
            var author = authors.FirstOrDefault(a => a.Id == id || id == null);
            ViewBag.AuthorId = author != null ? author.Id : 0;
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Authors = authors;
            return View(new Product { SortOrder = (_context.Products.Max(c => (int?)c.SortOrder) ?? 0) + 1 });
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(Product model, FormCollection collection)
        {
            try
            {
                var authors = _context.Authors.ToList();
                var author = authors.First(a => a.Id == model.AuthorId);
                ViewBag.AuthorId = author.Id;
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Authors = authors;

                var product = new Product();

                _context.Products.Add(product);
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return View(model);
            }
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Product/Edit/5

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
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Product/Delete/5

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
