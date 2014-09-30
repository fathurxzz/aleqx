using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
        [ValidateInput(false)] 
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var project = new Project();
                    TryUpdateModel(project, new[] { "Name", "Title", "DescriptionTitle", "SortOrder" });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);
                    project.VideoSource = form["VideoSource"];
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
                    return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
                }
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
        [ValidateInput(false)] 
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var project = context.Project.First(p => p.Id == id);
                    TryUpdateModel(project, new[] { "Name", "Title", "DescriptionTitle", "SortOrder" });
                    project.Description = HttpUtility.HtmlDecode(form["Description"]);
                    project.VideoSource = form["VideoSource"];
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

        [ActionName("AddImageToProject")]
        public ActionResult AddImageToProject(int id)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == id);

                ViewBag.projectId = project.Id;
                ViewBag.projectName = project.Name;
            }
            return View();
        }

        [HttpPost]
        [ActionName("AddImageToProject")]
        public ActionResult AddImageToProject1(int projectId)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == projectId);

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;

                    var pi = new ProjectImage();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    pi.ImageSource = fileName;
                    project.ProjectImages.Add(pi);
                    context.SaveChanges();
                }

                

                //if (fileUpload != null)
                //{
                //    var pi = new ProjectImage();
                //    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                //    string filePath = Server.MapPath("~/Content/Images");
                //    filePath = Path.Combine(filePath, fileName);
                //    fileUpload.SaveAs(filePath);
                //    pi.ImageSource = fileName;
                //    project.ProjectImages.Add(pi);
                //    context.SaveChanges();
                //}
                return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
            }
        }

        public ActionResult AddFlashToProject(int id)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == id);

                ViewBag.projectId = project.Id;
                ViewBag.projectName = project.Name;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddFlashToProject(FlashContent model, int projectId, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == projectId);

                
                var file = fileUpload;
                if (file != null)
                {

                    var pi = new FlashContent();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Flash", file.FileName);
                    string filePath = Server.MapPath("~/Content/Flash");
                    string flashSourceFilePath = filePath;
                    string flashDestFilePath = filePath;

                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    pi.ImageSource = fileName;
                    pi.Title = model.Title;
                    project.FlashContents.Add(pi);
                    
                    flashSourceFilePath = Path.Combine(flashSourceFilePath, "virtualtour.xml");
                    flashDestFilePath = Path.Combine(flashDestFilePath, Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    System.IO.File.Copy(flashSourceFilePath, flashDestFilePath);

                    context.SaveChanges();
                }


                //if (fileUpload != null)
                //{
                //    var pi = new ProjectImage();
                //    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                //    string filePath = Server.MapPath("~/Content/Images");
                //    filePath = Path.Combine(filePath, fileName);
                //    fileUpload.SaveAs(filePath);
                //    pi.ImageSource = fileName;
                //    project.ProjectImages.Add(pi);
                //    context.SaveChanges();
                //}
                return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
            }
        }

        public ActionResult AddSongToProject(int id)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == id);

                ViewBag.projectId = project.Id;
                ViewBag.projectName = project.Name;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddSongToProject(Song model, int projectId, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.First(p => p.Id == projectId);
                var file = fileUpload;
                if (file != null)
                {

                    var pi = new Song();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Music/mp3", file.FileName);
                    string filePath = Server.MapPath("~/Content/Music/mp3");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    pi.FileName = fileName;
                    pi.Title = model.Title;
                    project.Songs.Add(pi);
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

        public ActionResult DeleteFlash(int id)
        {
            using (var context = new SiteContainer())
            {
                var projectImage = context.FlashContent.Include("Project").First(pi => pi.Id == id);
                var project = projectImage.Project;
                IOHelper.DeleteFile("~/Content/Flash", projectImage.ImageSource);
                IOHelper.DeleteFile("~/Content/Flash", projectImage.ImageSource, "xml");
                context.DeleteObject(projectImage);
                context.SaveChanges();
                return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
            }
        }

        public ActionResult DeleteSong(int id)
        {
            using (var context = new SiteContainer())
            {
                var projectImage = context.Song.Include("Project").First(pi => pi.Id == id);
                var project = projectImage.Project;
                IOHelper.DeleteFile("~/Content/Music/mp3", projectImage.FileName);
                context.DeleteObject(projectImage);
                context.SaveChanges();
                return RedirectToAction("Projects", "Home", new { area = "", id = project.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var project = context.Project.Include("ProjectImages").First(p => p.Id == id);

                while (project.ProjectImages.Any())
                {
                    var projectImage = project.ProjectImages.First();
                    IOHelper.DeleteFile("~/Content/Images", projectImage.ImageSource);
                    foreach (var thumbnail in SiteSettings.Thumbnails)
                    {
                        IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, projectImage.ImageSource);
                    }

                    context.DeleteObject(projectImage);
                }

                while (project.FlashContents.Any())
                {
                    var projectImage = project.FlashContents.First();
                    IOHelper.DeleteFile("~/Content/Flash", projectImage.ImageSource);
                    IOHelper.DeleteFile("~/Content/Flash", projectImage.ImageSource, "xml");

                    context.DeleteObject(projectImage);
                }
                
                
                IOHelper.DeleteFile("~/Content/Images", project.ImageSource);
                foreach (var thumbnail in SiteSettings.Thumbnails)
                {
                    IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, project.ImageSource);
                }


                context.DeleteObject(project);
                context.SaveChanges();

                return RedirectToAction("Projects", "Home", new { area = "" });
            }
        }
    }
}
