using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;

namespace Penetron.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        SiteContext _context;

        public CategoryController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View(_context.Category.ToList());
        }

    }
}
