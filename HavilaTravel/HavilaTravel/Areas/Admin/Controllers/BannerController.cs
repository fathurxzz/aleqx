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
            return RedirectToAction("Index", "Home",new{Area=""});
        }

    }
}
