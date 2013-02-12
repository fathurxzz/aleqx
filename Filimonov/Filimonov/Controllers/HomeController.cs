using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Filimonov.Controllers
{
    [HandleError()]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Projects(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model);
            }
        }

        public ActionResult ProjectDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model.Project);
            }
        }

        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        [HttpPost]
        public void UpdateProjectImage(string fileName)
        {
            GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("projectImage"));
        }
    }
}
