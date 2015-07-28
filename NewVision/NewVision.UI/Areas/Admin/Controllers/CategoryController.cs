using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly SiteContext _context;

        public CategoryController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        //
        // GET: /Admin/Category/Details/5

        public ActionResult Details(int id)
        {
            var category = _context.Categories.First(c => c.Id == id);
            ViewBag.Category = category;

            ViewBag.AuthorCategories = _context.AuthorCategories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Details(FormCollection form, int categoryId)
        {
            try
            {
                var category = _context.Categories.First(c => c.Id == categoryId);



                PostCheckboxesData attrData = form.ProcessPostCheckboxesData("category", "categoryId");
                foreach (var kvp in attrData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;


                    var ac = _context.AuthorCategories.First(t => t.Id == tagId);

                    if (tagValue)
                    {
                        if (!category.AuthorCategories.Contains(ac))
                            category.AuthorCategories.Add(ac);
                    }
                    else
                    {
                        if (category.AuthorCategories.Contains(ac))
                        {
                            category.AuthorCategories.Remove(ac);
                        }
                    }
                }


                PostCheckboxesData tagData = form.ProcessPostCheckboxesData("tag", "categoryId");
                foreach (var kvp in tagData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;


                    var tag = _context.Tags.First(t => t.Id == tagId);

                    if (tagValue)
                    {
                        if (!category.Tags.Contains(tag))
                            category.Tags.Add(tag);
                    }
                    else
                    {
                        if (category.Tags.Contains(tag))
                        {
                            category.Tags.Remove(tag);
                        }
                    }
                }

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
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            return View(new Category { SortOrder = (_context.Categories.Max(c => (int?)c.SortOrder) ?? 0) + 1 });
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(Category model)
        {
            try
            {
                var category = new Category
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    SortOrder = model.SortOrder
                };

                _context.Categories.Add(category);
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
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id)
        {
            var tag = _context.Categories.First(t => t.Id == id);
            return View(tag);
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Category model)
        {
            try
            {
                var tag = _context.Categories.FirstOrDefault(t => t.Id == id);
                if (tag != null)
                {
                    tag.Title = model.Title;
                    tag.TitleEn = model.TitleEn;
                    tag.TitleUa = model.TitleUa;
                    tag.SortOrder = model.SortOrder;
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

        //
        // GET: /Admin/Category/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                //var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
                //if (tag != null)
                //{
                //    tag.Categories.Clear();
                //    tag.Products.Clear();
                //    tag.Authors.Clear();
                //    _context.Tags.Remove(tag);
                //    _context.SaveChanges();
                //}

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
