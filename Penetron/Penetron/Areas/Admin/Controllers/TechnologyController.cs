using System;
using System.Collections.Generic;
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
        public ActionResult Create(FormCollection form, int? parentId)
        {
            try
            {
                var technology = new Technology();
                TryUpdateModel(technology, new[] {"Name", "Title","SortOrder", "SeoDescription", "SeoKeywords" });
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
            catch
            {
                return View();
            }
        }

    }
}
