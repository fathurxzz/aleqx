using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Admin/Project/

        public ActionResult Create()
        {
            return View(new Project());
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var project = new Project { ImageSource = "" };
                TryUpdateModel(project,
                               new[]
                                   {
                                       "Name",
                                       "Title",
                                       "SortOrder"
                                   });

                project.TextTop = HttpUtility.HtmlDecode(form["TextTop"]);
                project.TextBottom = HttpUtility.HtmlDecode(form["TextBottom"]);

                context.AddToProject(project);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "", id = "projects" });
            }
        }

    }
}
