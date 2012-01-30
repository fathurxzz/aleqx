using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zelenuk.Models;

namespace Zelenuk.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var galleries = context.Gallery.ToList();
                ViewBag.Menu = Menu.GetMenuFromContext(context, "Gallery");
                return View(galleries);
            }
        }

        public ActionResult Details(string id)
        {
            using (var context = new ContentStorage())
            {
                ViewBag.Menu = Menu.GetMenuFromContext(context, "Gallery");
                var gallery = context.Gallery.Include("GalleryItems").Where(g=>g.Name==id).First();
                return View(gallery);
            }
        }

      

    }
}
