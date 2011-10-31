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
    public class BannerController : Controller
    {
        //
        // GET: /Admin/Banner/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var content = context.Banner.ToList();
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
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Banners", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Banners");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);

                    var banner = new Banner();
                    TryUpdateModel(banner, new[]
                                               {
                                                   "Price",
                                                   "Title"
                                               });
                    banner.BannerType = Convert.ToInt32(form["BannerType"]);
                    banner.ImageSource = fileName;
                    context.AddToBanner(banner);
                    context.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Banner", new { Area = "Admin" });
        }

        public ActionResult Edit(int id)
        {
            using (var context = new ContentStorage())
            {
                var banner = context.Banner.Where(b => b.Id == id).First();
                return View(banner);
            }
        }

        [HttpPost]
        public ActionResult Edit(Banner model, FormCollection form, HttpPostedFileBase fileUpload)
        {
            using (var context = new ContentStorage())
            {
                var banner = context.Banner.Where(b => b.Id == model.Id).First();
                string fileName = banner.ImageSource;

                if (fileUpload != null)
                {
                    IOHelper.DeleteFile("~/Content/Banners", banner.ImageSource);

                    fileName = IOHelper.GetUniqueFileName("~/Content/Banners", fileUpload.FileName);
                    string filePath = Server.MapPath("~/Content/Banners");
                    filePath = Path.Combine(filePath, fileName);
                    fileUpload.SaveAs(filePath);
                }
                TryUpdateModel(banner, new[] { "Price", "Title" });
                banner.BannerType = Convert.ToInt32(form["BannerType"]);
                banner.ImageSource = fileName;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Banner", new { Area = "Admin" });
        }

        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var banner = context.Banner.Where(b => b.Id == id).First();
                IOHelper.DeleteFile("~/Content/Banners", banner.ImageSource);
                context.DeleteObject(banner);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Banner", new { Area = "Admin" });
        }

    }
}
