using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Create(int id)
        {
            using (var context = new ModelContainer())
            {
                var album = context.Album.First(a => a.Id == id);
                ViewBag.AlbumId = album.Id;
                ViewBag.AlbumName = album.Name;
                ViewBag.AlbumTitle = album.Title;
                return View(new Product());
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase uploadFile)
        {
            using (var context = new ModelContainer())
            {
                int albumId = Convert.ToInt32(form["AlbumId"]);
                var album = context.Album.First(a => a.Id == albumId);

                var product = new Product ();
                TryUpdateModel(product,
                               new[]
                                   {
                                       "Title",
                                       "SortOrder"
                                   });

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                string filePath = Server.MapPath("~/Content/Images");
                filePath = Path.Combine(filePath, fileName);
                uploadFile.SaveAs(filePath);
                
                product.ImageSource = fileName;
                album.Products.Add(product);
                
                context.SaveChanges();
                return RedirectToAction("Catalogue", "Home", new { Area = "", id = album.Name });
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult SetDefault(int id)
        {
            using (var context = new ModelContainer())
            {
                var product = context.Product.Include("Album").First(p => p.Id == id);
                product.Album.ImageSource = product.ImageSource;
                context.SaveChanges();
                return RedirectToAction("Catalogue", "Home", new {Area = "", id = ""});
            }
        }

    }
}
