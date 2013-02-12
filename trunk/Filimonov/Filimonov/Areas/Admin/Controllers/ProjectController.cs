﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                var projects = context.Project.ToList();
                var sortOrder = projects.Any() ? projects.Max(p => p.SortOrder) : 0;
                return View(new Project { SortOrder = sortOrder + 1 });
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var project = new Project();
                    TryUpdateModel(project, new[] { "Name", "Title", "DescriptionTitle", "SortOrder" });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);
                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        project.ImageSource = fileName;
                    }
                    context.AddToProject(project);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == id);
                return View(project);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var project = context.Project.First(p => p.Id == id);
                    TryUpdateModel(project, new[] { "Name", "Title", "DescriptionTitle", "SortOrder" });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);
                    if (fileUpload != null)
                    {
                        if (!string.IsNullOrEmpty(project.ImageSource))
                        {

                            IOHelper.DeleteFile("~/Content/Images", project.ImageSource);
                            foreach (var thumbnail in SiteSettings.Thumbnails)
                            {
                                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, project.ImageSource);
                            }
                        }

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        project.ImageSource = fileName;
                    }
                    context.SaveChanges();
                    return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult AddImageToProject(int id)
        {
            ViewBag.projectId = id;

            return View();
        }

        [HttpPost]
        public ActionResult AddImageToProject(int projectId, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == projectId);
                if (fileUpload != null)
                {
                    var pi = new ProjectImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    pi.ImageSource = fileName;
                    project.ProjectImages.Add(pi);
                    context.SaveChanges();
                }
                return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
            }
        }

        public ActionResult DeleteImage(int id)
        {
            using (var context = new SiteContainer())
            {
                var projectImage = context.ProjectImage.Include("Project").First(pi => pi.Id == id);
                var project = projectImage.Project;
                IOHelper.DeleteFile("~/Content/Images", projectImage.ImageSource);
                foreach (var thumbnail in SiteSettings.Thumbnails)
                {
                    IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, projectImage.ImageSource);
                }
                context.DeleteObject(projectImage);
                context.SaveChanges();
                return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
            }
        }
    }
}
