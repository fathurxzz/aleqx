using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Helpers.Graphics;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class GalleryImageController : AdminController
    {
        //
        // GET: /Admin/GalleryImage/

        public GalleryImageController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            var galleryImages = _repository.GetGalleryImages();
            return View(galleryImages);
        }

        public ActionResult Create()
        {
            return View(new GalleryImage() );
        }

        [HttpPost]
        public ActionResult Create(GalleryImage model)
        {
            try
            {
                
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var siteImage = new GalleryImage();
                    var file = Request.Files[i];
                    if (file == null) continue;
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    siteImage.ImageSource = fileName;
                    _repository.AddGalleryImage(siteImage);
                }
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : "");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteGalleryImage(id, ImageHelper.DeleteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
