using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class TagController : AdminController
    {

        private readonly SiteContext _context;

        public TagController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }


        public ActionResult Create()
        {
            return View(new Tag());
        }


        [HttpPost]
        public ActionResult Create(Tag model)
        {
            try
            {
                var tag = new Tag
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                };

                _context.Tags.Add(tag);
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
        // GET: /Admin/Tag/Edit/5

        public ActionResult Edit(int id)
        {
            var tag = _context.Tags.First(t => t.Id == id);
            return View(tag);
        }

        //
        // POST: /Admin/Tag/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Tag model)
        {
            try
            {
                var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
                if (tag != null)
                {
                    tag.Title = model.Title;
                    tag.TitleEn = model.TitleEn;
                    tag.TitleUa = model.TitleUa;
                    _context.SaveChanges();
                }

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

        public ActionResult Delete(int id)
        {
            try
            {
                var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
                if (tag != null)
                {
                    tag.Categories.Clear();
                    tag.Products.Clear();
                    tag.Authors.Clear();
                    tag.AuthorCategories.Clear();
                    _context.Tags.Remove(tag);
                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return RedirectToAction("Index");
            }
        }
    }
}
