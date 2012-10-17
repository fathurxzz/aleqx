using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EM2013.Helpers;
using EM2013.Models;
using SiteExtensions;

namespace EM2013.Areas.Admin.Controllers
{
    [Authorize]
    public class SecretImageController : Controller
    {

        public ActionResult Create()
        {
            return View();
        } 


        [HttpPost]
        public ActionResult Create(FormCollection collection, List<HttpPostedFileBase> mainImage, List<HttpPostedFileBase> previewImage)
        {
            try
            {
                using (var context = new SiteContext())
                {
                    int imagesCount = previewImage.Count(file => file != null);


                    for (int i = 0; i < imagesCount; i++)
                    {
                        SecretImage si = new SecretImage();

                        string fileName = IOHelper.GetUniqueFileName("~/Content/Images", previewImage[i].FileName);
                        string filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        previewImage[i].SaveAs(filePath);
                        si.PreviewImageSource = fileName;

                        fileName = IOHelper.GetUniqueFileName("~/Content/Images", mainImage[i].FileName);
                        filePath = Server.MapPath("~/Content/Images");
                        filePath = Path.Combine(filePath, fileName);
                        mainImage[i].SaveAs(filePath);
                        si.ImageSource = fileName;

                        context.AddToSecretImage(si);
                        context.SaveChanges();
                    }
                    return RedirectToAction("SiteContent", "Home", new { id = "secretlink", area = "" });
                }
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            using (var context = new SiteContext())
            {
                var image = context.SecretImage.First(i => i.Id == id);
                ImageHelper.DeleteImage(image.ImageSource);
                ImageHelper.DeleteImage(image.PreviewImageSource);
                context.DeleteObject(image);
                context.SaveChanges();
                return RedirectToAction("SiteContent", "Home", new { id = "secretlink", area = "" });
            }
        }

    }
}
