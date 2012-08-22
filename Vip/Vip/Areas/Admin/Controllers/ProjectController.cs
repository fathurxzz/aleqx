using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteExtensions;
using SiteExtensions.Graphics;
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
                using (var context = new SiteContainer())
                {
                    var project = new Project();
                    TryUpdateModel(project, new[]
                                                {
                                                    "Name", 
                                                    "Title", 
                                                    "DescriptionTitle",
                                                    "SortOrder",
                                                    "Manager"
                                                });

                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        project.ImageSource = fileName;
                    }

                    project.Description = HttpUtility.HtmlDecode(form["Text"]);
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
            return View();
        }

        //
        // POST: /Admin/Project/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
            return View();
        }

        public ActionResult AddImage(int id)
        {
            using (var context = new SiteContainer())
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
                using (var context = new SiteContainer())
                {
                    var project = context.Project.First(p => p.Id == projectId);
                    if (fileUpload != null)
                    {

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);

                        GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("projectBigImage"), ScaleMode.Corp);
                        GraphicsHelper.SaveCachedImage("~/Content/Images", fileName, SiteSettings.GetThumbnail("projectDetailsPreviewThumbnail"), ScaleMode.Corp);



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
            using (var context = new SiteContainer())
            {
                var image = context.ProjectImage.Include("Project").First(pi => pi.Id == id);
                var projectName = image.Project.Name;

                IOHelper.DeleteFile("~/Content/Images", image.ImageSource);
                foreach (var thumbnail in SiteSettings.Thumbnails)
                {
                    IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, image.ImageSource);
                }

                context.DeleteObject(image);
                context.SaveChanges();

                return RedirectToAction("Index", "Projects", new { area = "", project = projectName });
            }
        }


    }
}
