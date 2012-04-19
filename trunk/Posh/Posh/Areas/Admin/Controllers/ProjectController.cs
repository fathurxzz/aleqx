using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
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
        public ActionResult Create(FormCollection form, HttpPostedFileBase uploadFile)
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

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                uploadFile.SaveAs(filePath);

                project.ImageSource = fileName;

                project.TextTop = HttpUtility.HtmlDecode(form["TextTop"]);
                project.TextBottom = HttpUtility.HtmlDecode(form["TextBottom"]);

                context.AddToProject(project);
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "", id = "projects" });
            }
        }


        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var project = context.Project.First(p => p.Id == id);
                return View(project);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            using (var context = new ModelContainer())
            {
                var project = context.Project.First(p => p.Id == id);
                TryUpdateModel(project,
                               new[]
                                   {
                                       "Name",
                                       "Title",
                                       "SortOrder"
                                   });
                project.TextTop = HttpUtility.HtmlDecode(form["TextTop"]);
                project.TextBottom = HttpUtility.HtmlDecode(form["TextBottom"]);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "projects" });
        }


        public ActionResult Delete(int id)
        {
            using (var context = new ModelContainer())
            {
                var project = context.Project.Include("ProjectItems").First(a => a.Id == id);
                if (!project.ProjectItems.Any())
                {
                    context.DeleteObject(project);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home", new { Area = "", id = "projects" });
        }


        public ActionResult SetDefault(int id)
        {
            using (var context = new ModelContainer())
            {
                var projectItem = context.ProjectItem.Include("Project").First(p => p.Id == id);
                projectItem.Project.ImageSource = projectItem.ImageSource;
                context.SaveChanges();
                return RedirectToAction("Index", "Home", new { Area = "", id = "projects" });
            }
        }


        public ActionResult CreateProjectItem(int id)
        {
            using (var context = new ModelContainer())
            {
                var project = context.Project.First(p => p.Id == id);
                ViewBag.ProjectId = project.Id;
                ViewBag.ProjectName = project.Name;
                ViewBag.ProjectTitle = project.Title;
            }
            return View(new ProjectItem());
        }

        [HttpPost]
        public ActionResult CreateProjectItem(FormCollection form, HttpPostedFileBase uploadFile)
        {
            using (var context = new ModelContainer())
            {
                int projectId = Convert.ToInt32(form["ProjectId"]);
                var project = context.Project.First(p => p.Id == projectId);

                var projectItem = new ProjectItem();
                TryUpdateModel(projectItem,
                               new[]
                                   {
                                       "Title",
                                       "SortOrder"
                                   });

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                uploadFile.SaveAs(filePath);

                GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, "mainView",ScaleMode.Corp);
                GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, "galleryThumbnail", ScaleMode.Corp);

                projectItem.ImageSource = fileName;

                project.ProjectItems.Add(projectItem);
                context.SaveChanges();

                return RedirectToAction("Index", "Projects", new { Area = "", id = project.Name });
            }
        }

        public ActionResult DeleteProjectItem(int id)
        {
            using (var context = new ModelContainer())
            {
                var projectItem = context.ProjectItem.Include("Project").First(pi => pi.Id == id);
                var projectName = projectItem.Project.Name;
                if (!string.IsNullOrEmpty(projectItem.ImageSource))
                {
                    IOHelper.DeleteFile("~/Content/Images", projectItem.ImageSource);
                    foreach (var folder in GraphicsHelper.ThumbnailFolders)
                    {
                        IOHelper.DeleteFile("~/ImageCache/" + folder, projectItem.ImageSource);
                    }
                }

                context.DeleteObject(projectItem);
                context.SaveChanges();
                return RedirectToAction("Index", "Projects", new {Area = "", id = projectName});
            }
        }

    }
}
