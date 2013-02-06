using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        
        public ActionResult Create()
        {
            using (var context = new SiteContainer())
            {
                var projects = context.Project.ToList();
                var sortOrder = projects.Any() ? projects.Max(p => p.SortOrder) : 0;
                return View(new Project {SortOrder = sortOrder + 1});
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
                    TryUpdateModel(project, new[] {"Name", "Title", "DescriptionTitle", "SortOrder"});
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

        //
        // POST: /Admin/Project/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
