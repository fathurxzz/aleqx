using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Babich.Models;
using Dev.Mvc.Helpers;

namespace Babich.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Admin/Gallery/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int id)
        {
            ViewData["id"] = id;
            using (var context = new ContentStorage())
            {
                var content = context.Content.Include("Galleries").Where(c => c.Id == id).First();
                int sortOrder = content.Galleries.Count > 0 ? content.Galleries.Max(c => c.SortOrder) : -1;
                ViewData["SortOrder"] = (sortOrder + 1).ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Add(int id, FormCollection form)
        {
            ViewData["id"] = id;

            using (var context = new ContentStorage())
            {
                var content = context.Content.Where(c => c.Id == id).First();

                var gallery = new Gallery
                {
                    Content = content,
                    Description = form["description"],
                    SortOrder = Convert.ToInt32(form["SortOrder"])
                };



                context.AddToGallery(gallery);
                context.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
            }

        }


        public ActionResult AddPhoto(int id)
        {
            ViewData["galleryId"] = id;
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Include("Content").Where(g => g.Id == id).First();
                ViewData["contentName"] = gallery.Content.Name;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(int galleryId, string contentName, FormCollection form)
        {
            using (var context = new ContentStorage())
            {
                var gallery = context.Gallery.Where(g => g.Id == galleryId).FirstOrDefault();
                
                if (Request.Files["logo"] != null && !string.IsNullOrEmpty(Request.Files["logo"].FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Photos", Request.Files["logo"].FileName);
                    string filePath = Server.MapPath("~/Content/Photos");
                    filePath = Path.Combine(filePath, fileName);
                    Request.Files["logo"].SaveAs(filePath);
                    gallery.GalleryItems.Add(new GalleryItem { ImageSource = fileName });
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Home", new { area = "", id = contentName, galleryId=galleryId });
            }
        }
    }
}
