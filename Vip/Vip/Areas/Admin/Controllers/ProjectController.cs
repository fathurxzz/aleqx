using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using SiteExtensions.Graphics;
using Vip.Helpers;
using Vip.Models;

namespace Vip.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {

   //
        // GET: /Admin/Project/Create

        public ActionResult Create()
        {
            return View(new Project { SortOrder = 0 });
        }

        //
        // POST: /Admin/Project/Create

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var project = new Project();
                    TryUpdateModel(project, new[]
                                                {
                                                    "Name", 
                                                    "Title", 
                                                    "DescriptionTitle",
                                                    "SortOrder",
                                                    "SeoDescription",
                                                    "SeoKeywords"
                                                });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);

                    context.AddToProject(project);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Projects", new { Area = "", project = project.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Project/Edit/5

        public ActionResult Edit(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var project = context.Project.First(p => p.Id == id);
                return View(project);
            }
        }

        //
        // POST: /Admin/Project/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var project = context.Project.First(p => p.Id == id);
                    TryUpdateModel(project, new[]
                                                {
                                                    "Name", 
                                                    "Title", 
                                                    "DescriptionTitle",
                                                    "SortOrder",
                                                    "SeoDescription",
                                                    "SeoKeywords"
                                                });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);

                    context.SaveChanges();
                    return RedirectToAction("Index", "Projects", new { Area = "", project = project.Name });
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Project/Delete/5

        public ActionResult Delete(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var project = context.Project.Include("ProjectImages").First(p => p.Id == id);
                while (project.ProjectImages.Any())
                {
                    var image = project.ProjectImages.First();
                    ImageHelper.DeleteImage(image.ImageSource);
                    context.DeleteObject(image);
                }
                
                context.DeleteObject(project);

                context.SaveChanges();
            }
            return RedirectToAction("Index", "Projects", new { Area = "" });
        }

        public ActionResult AddImage(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var project = context.Project.First(p => p.Id == id);
                ViewBag.ProjectId = project.Id;
                ViewBag.ProjectName = project.Name;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddImage(int projectId, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var project = context.Project.First(p => p.Id == projectId);
                    foreach (var file in fileUpload)
                    {
                        if (file != null)
                        {
                            string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                            string filePath = Server.MapPath("~/Content/Images");
                            filePath = Path.Combine(filePath, fileName);
                            file.SaveAs(filePath);
                            project.ProjectImages.Add(new ProjectImage { ImageSource = fileName });
                            context.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index", "Projects", new { area = "", project = project.Name });
                }
            }
            catch
            {
                return View();
            }

        }

        public ActionResult DeleteImage(int id)
        {
            using (var context = new CatalogueContainer())
            {
                var image = context.ProjectImage.Include("Project").First(pi => pi.Id == id);
                var projectName = image.Project.Name;

                ImageHelper.DeleteImage(image.ImageSource);

                context.DeleteObject(image);
                context.SaveChanges();

                return RedirectToAction("Index", "Projects", new { area = "", project = projectName });
            }
        }


    }
}
