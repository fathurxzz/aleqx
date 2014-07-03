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
        public ActionResult Index()
        {
            using (var context = new SiteContext())
            {
                var model = new CategoryModel(CurrentLang, context, null);
                this.SetSeoContent(model);
                return View(model);
            }
            
        }
    }
}
