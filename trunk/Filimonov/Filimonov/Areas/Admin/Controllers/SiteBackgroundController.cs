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
    [Authorize]
    public class SiteBackgroundController : Controller
    {
        //
        // GET: /Admin/SiteBackground/

        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var backgrounds = context.SiteBackground.ToList();
                return View(backgrounds);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(SiteBackground model)
        {
            using (var context = new SiteContainer())
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;

                    var pi = new SiteBackground();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    pi.ImageSource = fileName;
                    context.AddToSiteBackground(pi);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var projectImage = context.SiteBackground.First(pi => pi.Id == id);
                IOHelper.DeleteFile("~/Content/Images", projectImage.ImageSource);
                foreach (var thumbnail in SiteSettings.Thumbnails)
                {
                    IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, projectImage.ImageSource);
                }
                context.DeleteObject(projectImage);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
