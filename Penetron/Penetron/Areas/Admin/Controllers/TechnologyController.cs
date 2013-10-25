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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, int? parentId)
        {
            try
            {
                var technology = new Technology();
                TryUpdateModel(technology, new[] { "Title", "SeoDescription", "SeoKeywords" });
                technology.Text = HttpUtility.HtmlDecode(form["Text"]);
                _context.AddToTechnology(technology);
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
