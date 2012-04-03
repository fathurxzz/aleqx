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

                ViewBag.Categories = context.Category.ToList();
                ViewBag.Elements = context.Element.ToList();

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

                PostCheckboxesData cbDataCategories = form.ProcessPostCheckboxesData("category");
                PostCheckboxesData cbDataElements = form.ProcessPostCheckboxesData("element");



                var product = new Product();
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

                foreach (var kvp in cbDataCategories)
                {
                    var categoryId = kvp.Key;
                    bool productValue = kvp.Value;
                    if (productValue)
                    {
                        var category = context.Category.First(c => c.Id == categoryId);
                        product.Categories.Add(category);
                    }
                }

                foreach (var kvp in cbDataElements)
                {
                    var elementId = kvp.Key;
                    var elemrntValue = kvp.Value;
                    if (elemrntValue)
                    {
                        var element = context.Element.First(e => e.Id == elementId);
                        product.Elements.Add(element);
                    }
                }


                album.Products.Add(product);

                context.SaveChanges();
                return RedirectToAction("Index", "Albums", new { Area = "", id = album.Name });
            }
        }



        public ActionResult Edit(int id)
        {
            using (var context = new ModelContainer())
            {
                var product = context.Product.Include("Album").Include("Categories").Include("Elements").First(p => p.Id == id);


                var album = product.Album;
                ViewBag.AlbumId = album.Id;
                ViewBag.AlbumName = album.Name;
                ViewBag.AlbumTitle = album.Title;

                ViewBag.Categories = context.Category.ToList();
                ViewBag.Elements = context.Element.ToList();



                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form, HttpPostedFileBase uploadFile)
        {
            using (var context = new ModelContainer())
            {
                int albumId = Convert.ToInt32(form["AlbumId"]);
                var album = context.Album.First(a => a.Id == albumId);

                PostCheckboxesData cbDataCategories = form.ProcessPostCheckboxesData("category");
                PostCheckboxesData cbDataElements = form.ProcessPostCheckboxesData("element");



                var product = context.Product.First(p=>p.Id==id);
                TryUpdateModel(product,
                               new[]
                                   {
                                       "Title",
                                       "SortOrder"
                                   });
                if (uploadFile != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageSource))
                    {
                        IOHelper.DeleteFile("~/Content/Images", product.ImageSource);
                        foreach (var folder in GraphicsHelper.ThumbnailFolders)
                        {
                            IOHelper.DeleteFile("~/ImageCache/" + folder, product.ImageSource);
                        }
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", uploadFile.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    uploadFile.SaveAs(filePath);

                    product.ImageSource = fileName;
                }

                product.Categories.Clear();
                foreach (var kvp in cbDataCategories)
                {
                    var categoryId = kvp.Key;
                    bool productValue = kvp.Value;
                    if (productValue)
                    {
                        var category = context.Category.First(c => c.Id == categoryId);
                        product.Categories.Add(category);
                    }
                }

                product.Elements.Clear();
                foreach (var kvp in cbDataElements)
                {
                    var elementId = kvp.Key;
                    var elemrntValue = kvp.Value;
                    if (elemrntValue)
                    {
                        var element = context.Element.First(e => e.Id == elementId);
                        product.Elements.Add(element);
                    }
                }


                album.Products.Add(product);

                context.SaveChanges();
                return RedirectToAction("Index", "Albums", new { Area = "", id = album.Name });
            }
        }






        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Albums", new { Area = "", id = "" });
        }

        public ActionResult SetDefault(int id)
        {
            using (var context = new ModelContainer())
            {
                var product = context.Product.Include("Album").First(p => p.Id == id);
                product.Album.ImageSource = product.ImageSource;
                context.SaveChanges();
                return RedirectToAction("Index", "Albums", new { Area = "", id = "" });
            }
        }

    }
}
