using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using Vip.Models;

namespace Vip.Controllers
{
    public class ProjectsController : Controller
    {
        //
        // GET: /Projects/

        public ActionResult Index(string project)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectViewModel(context, project);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.Menu;
                return model.Project != null ? View("Details", model) : View(model);
            }
        }

    }
}
