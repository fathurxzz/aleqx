using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poggen.Models;
using SiteExtensions;

namespace Poggen.Controllers
{
    public class ProjectController : Controller
    {

        public ActionResult Index(string category, string subcategory)
        {
            using (var context = new SiteContainer())
            {
                var model = new CatalogueViewModel(context, category, subcategory, (int)CategoryType.Project);
                ViewBag.CategoryType = (int)CategoryType.Project;
                this.SetSeoContent(model);
                if (model.Category != null)
                {
                    ViewBag.CategoryId = model.Category.Id;
                }
                return View(model);
            }
        }

    }
}
