﻿using System;
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
            using (var context = new CatalogueContainer())
            {
                var model = new ProjectViewModel(context, project);
                ViewBag.Categories = model.Categories;
                ViewBag.Projects = model.Projects;
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.Menu;
                ViewBag.ProjectsPage = true;
                return View(model);
            }
        }

    }
}
