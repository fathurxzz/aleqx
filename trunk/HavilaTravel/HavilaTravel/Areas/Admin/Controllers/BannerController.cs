using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Create(FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var banner = new Banner();
                TryUpdateModel(banner, new[]
                                           {
                                               "Price",
                                               "Title",
                                               "BannerType"
                                           });
                
                context.AddToBanner(banner);
                context.SaveChanges();
                return View();
            }
        }

    }
}
