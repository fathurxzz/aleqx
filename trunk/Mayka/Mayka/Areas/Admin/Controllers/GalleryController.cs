using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Mayka.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        private readonly SiteContext _context;

        public GalleryController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int id)
        {
            var content = _context.Content.First(c => c.Id == id);
            return View(new PhotoGalleryItem {Content = content,ContentId = id});
        }

        [HttpPost]
        public ActionResult Create(PhotoGalleryItem model, HttpPostedFileBase fileUpload)
        {
            var content = _context.Content.First(c => c.Id == model.ContentId);
            var pgi = new PhotoGalleryItem {Content = content};
            
            TryUpdateModel(pgi, new[] { "Title", "Url" });
            
            if (fileUpload != null)
            {
                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", fileUpload.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                GraphicsHelper.SaveOriginalImage(filePath, fileName, fileUpload);
                //fileUpload.SaveAs(filePath);
                pgi.ImageSource = fileName;
            }

            _context.PhotoGalleryItem.Add(pgi);
            _context.SaveChanges();

            return RedirectToAction("Gallery", "Home", new {area = "", id = content.Name});
        }
    }
}
