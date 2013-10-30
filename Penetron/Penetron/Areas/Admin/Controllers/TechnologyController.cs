using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Penetron.Models;

namespace Penetron.Areas.Admin.Controllers
{
    public class TechnologyController : Controller
    {
        private SiteContext _context;

        public TechnologyController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                ViewBag.ParentId = id.Value;
            return View(new Technology());
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Create(FormCollection form, int? parentId)
        {
            var technology = new Technology
                             {
                                 Name = Utils.Transliterator.Transliterate(form["Name"])
                             };
            TryUpdateModel(technology, new[] { "Title", "SortOrder", "SeoDescription", "SeoKeywords" });
            technology.CategoryLevel = 1;
            technology.Active = true;
            technology.Text = HttpUtility.HtmlDecode(form["Text"]);

            if (parentId.HasValue)
            {
                var parent = _context.Technology.First(c => c.Id == parentId);
                parent.Children.Add(technology);
            }
            else
            {
                _context.AddToTechnology(technology);
            }
            _context.SaveChanges();
            return RedirectToAction("Technologies", "Home", new { area = "" });
        }



        public ActionResult Edit(int id)
        {
            var technology = _context.Technology.First(t => t.Id == id);
            return View(technology);
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(Exception), View = "DbError")]
        public ActionResult Edit(Technology model)
        {
            var technology = _context.Technology.First(t => t.Id == model.Id);


            return RedirectToAction("Technologies", "Home", new { area = "" });
        }


    }
}
