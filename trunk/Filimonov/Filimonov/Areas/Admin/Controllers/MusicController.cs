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
    public class MusicController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var backgrounds = context.MusicItem.ToList();
                return View(backgrounds);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MusicItem model, HttpPostedFileBase fileUpload)
        {
            using (var context = new SiteContainer())
            {
                var file = fileUpload;
                if (file != null)
                {
                    var pi = new MusicItem();
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Music/mp3", file.FileName);
                    string filePath = Server.MapPath("~/Content/Music/mp3");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    pi.FileName = fileName;
                    context.AddToMusicItem(pi);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var projectImage = context.MusicItem.First(pi => pi.Id == id);
                IOHelper.DeleteFile("~/Content/Music/mp3", projectImage.FileName);
                context.DeleteObject(projectImage);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
