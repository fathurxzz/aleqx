using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class AuthorCategoryController : AdminController
    {
        //
        // GET: /Admin/AuthorCategory/

        private readonly SiteContext _context;

        public AuthorCategoryController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.AuthorCategories.ToList());
        }

        //
        // GET: /Admin/AuthorCategory/Details/5

        public ActionResult Details(int id)
        {
            var category = _context.AuthorCategories.First(c => c.Id == id);
            ViewBag.AuthorCategory = category;
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Details(FormCollection form, int categoryId)
        {
            try
            {
                var category = _context.AuthorCategories.First(c => c.Id == categoryId);



                PostCheckboxesData categoryData = form.ProcessPostCheckboxesData("category","categoryId");
                foreach (var kvp in categoryData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;


                    var ac = _context.Categories.First(t => t.Id == tagId);

                    if (tagValue)
                    {
                        if (!category.Categories.Contains(ac))
                            category.Categories.Add(ac);
                    }
                    else
                    {
                        if (category.Categories.Contains(ac))
                        {
                            category.Categories.Remove(ac);
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
        // GET: /Admin/AuthorCategory/Create

        public ActionResult Create()
        {
            return View(new AuthorCategory { SortOrder = (_context.AuthorCategories.Max(c => (int?)c.SortOrder) ?? 0) + 1 });
        }

        //
        // POST: /Admin/AuthorCategory/Create

        [HttpPost]
        public ActionResult Create(AuthorCategory model)
        {
            try
            {
                var category = new AuthorCategory
                {
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    SortOrder = model.SortOrder
                };

                _context.AuthorCategories.Add(category);
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
        // GET: /Admin/AuthorCategory/Edit/5

        public ActionResult Edit(int id)
        {
            var tag = _context.AuthorCategories.First(t => t.Id == id);
            return View(tag);
        }

        //
        // POST: /Admin/AuthorCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AuthorCategory model)
        {
            try
            {
                var tag = _context.AuthorCategories.FirstOrDefault(t => t.Id == id);
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

        public ActionResult Delete(int id)
        {
            try
            {
                var ac = _context.AuthorCategories.FirstOrDefault(t => t.Id == id);
                if (ac != null)
                {
                    ac.Tags.Clear();
                    ac.Categories.Clear();
                    _context.AuthorCategories.Remove(ac);
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
