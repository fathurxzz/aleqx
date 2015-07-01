using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class AuthorController : AdminController
    {
        private readonly SiteContext _context;

        public AuthorController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        //
        // GET: /Admin/Author/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Author/Create

        public ActionResult Create()
        {
            return View(new Author { SortOrder = (_context.Authors.Max(c => (int?)c.SortOrder) ?? 0)+1 });
        }

        //
        // POST: /Admin/Author/Create

        [HttpPost]
        public ActionResult Create(Author model, HttpPostedFileBase photo, HttpPostedFileBase avatar)
        {
            try
            {
                var author = new Author
                {
                    Name = model.Name,
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    Description = model.Description,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionUa = model.DescriptionUa,
                    About = model.About,
                    AboutEn = model.AboutEn,
                    AboutUa = model.AboutUa
                };

                _context.Authors.Add(author);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Author/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Author/Edit/5

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
        // GET: /Admin/Author/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Author/Delete/5

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
