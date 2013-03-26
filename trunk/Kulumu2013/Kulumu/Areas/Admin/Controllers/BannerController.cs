using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kulumu.Models;
using SiteExtensions;

namespace Kulumu.Areas.Admin.Controllers
{
    [Authorize]
    public class BannerController : Controller
    {
        
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var banners = context.Banner.ToList();
                return View(banners);
            }
        }


        public ActionResult Create()
        {
            return View();
        } 


        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var banner = new Banner();
                    TryUpdateModel(banner, new[] { "Description", "Link", "Price" });
                    if (fileUpload != null)
                    {
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        banner.ImageSource = fileName;
                    }
                    context.AddToBanner(banner);
                    context.SaveChanges();
                    return RedirectToAction("Index");
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
                var banner = context.Banner.First(b => b.Id == id);
                return View(banner);
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            try
            {
                using (var context = new SiteContainer())
                {
                    var banner = context.Banner.First(b => b.Id == id);
                    TryUpdateModel(banner, new[] { "Description", "Link", "Price" });

                    if (fileUpload != null)
                    {
                        if (!string.IsNullOrEmpty(banner.ImageSource))
                        {

                            IOHelper.DeleteFile("~/Content/Images", banner.ImageSource);
                            foreach (var thumbnail in SiteSettings.Thumbnails)
                            {
                                IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, banner.ImageSource);
                            }
                        }
                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        fileUpload.SaveAs(filePath);
                        banner.ImageSource = fileName;
                    }
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

 
        public ActionResult Delete(int id)
        {
            using (var context = new SiteContainer())
            {
                var banner = context.Banner.First(b => b.Id == id);
                IOHelper.DeleteFile("~/Content/Images", banner.ImageSource);
                foreach (var thumbnail in SiteSettings.Thumbnails)
                {
                    IOHelper.DeleteFile("~/ImageCache/" + thumbnail.Key, banner.ImageSource);
                }
                context.DeleteObject(banner);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
