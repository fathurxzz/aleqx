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
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
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
                                                    "Manager",
                                                    "SeoDescription",
                                                    "SeoKeywords"
                                                });
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
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
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
                                                    "Manager",
                                                    "SeoDescription",
                                                    "SeoKeywords"
                                                });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);


                    if (fileUpload != null)
                    {
                        ImageHelper.DeleteImage(project.ImageSource);
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        project.ImageSource = fileName;
                    }

                    context.SaveChanges();
                    return RedirectToAction("Index", "Projects", new { Area = "" });
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

                ImageHelper.DeleteImage(project.ImageSource);
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
        public ActionResult AddImage(int projectId, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new CatalogueContainer())
                {
                    var project = context.Project.First(p => p.Id == projectId);
                    if (fileUpload != null)
                    {

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);

                        GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("projectBigImage"), ScaleMode.Crop);
                        GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("projectDetailsPreviewThumbnail"), ScaleMode.Crop);



                        var projectImage = new ProjectImage { ImageSource = fileName };
                        project.ProjectImages.Add(projectImage);
                        context.SaveChanges();
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
