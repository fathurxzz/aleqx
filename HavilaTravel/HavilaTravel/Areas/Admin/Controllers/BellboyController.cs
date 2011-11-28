using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HavilaTravel.Helpers;
using HavilaTravel.Models;

namespace HavilaTravel.Areas.Admin.Controllers
{
    [Authorize]
    public class BellboyController : Controller
    {
        //
        // GET: /Admin/Bellboy/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var content = context.Bellboy.ToList();
                return View(content);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                if (fileUpload != null)
                {
                    
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Bellboy", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Bellboy");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                    
                    var bellboy = new Bellboy();
                    TryUpdateModel(bellboy, new[] { "TopText", "TopLink", "BottomText", "BottomLink" });
                    bellboy.ImageSource = fileName;
                    context.AddToBellboy(bellboy);
                    context.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Bellboy", new { Area = "Admin" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var bellboy = context.Bellboy.Where(b => b.Id == id).First();
                return View(bellboy);
            }
        }

        [HttpPost]
        public ActionResult Edit(Banner model, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                var bellboy = context.Bellboy.Where(b => b.Id == model.Id).First();
                string fileName = bellboy.ImageSource;

                if (fileUpload != null)
                {
                    IOHelper.DeleteFile("~/Content/Bellboy", bellboy.ImageSource);

                    fileName = IOHelper.GetUniqueFileName("~/Content/Bellboy", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Bellboy");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                }
                TryUpdateModel(bellboy, new[] { "TopText", "TopLink", "BottomText", "BottomLink" });
                bellboy.ImageSource = fileName;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Bellboy", new { Area = "Admin" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var bellboy = context.Bellboy.Where(b => b.Id == id).First();
                IOHelper.DeleteFile("~/Content/Bellboy", bellboy.ImageSource);
                context.DeleteObject(bellboy);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Bellboy", new { Area = "Admin" });
        }
    }
}
