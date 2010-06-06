using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
{
    public class GalleriesController : Controller
    {
        //
        // GET: /Galleries/

        public ActionResult Index(int id)
        {
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Include("Content").Include("GalleryItems").Where(g => g.Id == id).Select(g => g).FirstOrDefault();
                return View(gallery);
            }
        }

    }
}
