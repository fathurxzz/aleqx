using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;
using SiteExtensions;

namespace Leo.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
         
                var model = new CategoryModel(CurrentLang, _context, null);
                this.SetSeoContent(model);
                return View(model);
            
            
        }
    }
}
