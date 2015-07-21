using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewVision.UI.Helpers;
using NewVision.UI.Models;

namespace NewVision.UI.Areas.Admin.Controllers
{
    public class AuthorController : AdminController
    {
        private readonly SiteContext _context;

        public AuthorController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        //
        // GET: /Admin/Author/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Author/Create

        public ActionResult Create()
        {

            ViewBag.Tags = _context.Tags.ToList();

            return View(new Author { SortOrder = (_context.Authors.Max(c => (int?)c.SortOrder) ?? 0)+1 });



        }

        //
        // POST: /Admin/Author/Create

        [HttpPost]
        public ActionResult Create(Author model, HttpPostedFileBase photo, HttpPostedFileBase avatar, FormCollection form)
        {
            try
            {
                ViewBag.Tags = _context.Tags.ToList();
                var author = new Author
                {
                    Name = model.Name,
                };

                author.Title = model.Title == null ? "" : HttpUtility.HtmlDecode(model.Title);
                author.TitleEn = model.TitleEn == null ? "" : HttpUtility.HtmlDecode(model.TitleEn);
                author.TitleUa = model.TitleUa == null ? "" : HttpUtility.HtmlDecode(model.TitleUa);

                author.Description = model.Description == null ? "" : HttpUtility.HtmlDecode(model.Description);
                author.DescriptionEn = model.DescriptionEn == null ? "" : HttpUtility.HtmlDecode(model.DescriptionEn);
                author.DescriptionUa = model.DescriptionUa == null ? "" : HttpUtility.HtmlDecode(model.DescriptionUa);

                author.About = model.About == null ? "" : HttpUtility.HtmlDecode(model.About);
                author.AboutEn = model.AboutEn == null ? "" : HttpUtility.HtmlDecode(model.AboutEn);
                author.AboutUa = model.AboutUa == null ? "" : HttpUtility.HtmlDecode(model.AboutUa);




                if (photo != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images/author", photo.FileName);
                    string filePath = Server.MapPath("~/Content/Images/author");
                    string filePathThumb = Server.MapPath("~/Content/Images/author/thumb");
                    filePath = Path.Combine(filePath, fileName);
                    filePathThumb = Path.Combine(filePathThumb, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, photo, 670);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, photo, 670, 670, ScaleMode.Crop);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePathThumb, fileName, photo, 324, 324, ScaleMode.Crop);
                    author.Photo = fileName;
                }

                if (avatar != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images/author", avatar.FileName);
                    string filePath = Server.MapPath("~/Content/Images/author");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, photo, 670);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, avatar, 150, 150, ScaleMode.Crop);
                    author.Avatar = fileName;
                }


                PostCheckboxesData attrData = form.ProcessPostCheckboxesData("tag");

                foreach (var kvp in attrData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;

                    //author.Tags.Clear();

                    if (tagValue)
                    {
                        var tag = _context.Tags.First(t => t.Id == tagId);
                        author.Tags.Add(tag);
                    }
                }


                _context.Authors.Add(author);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return View(model);
            }
        }

        //
        // GET: /Admin/Author/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Tags = _context.Tags.ToList();
            var author = _context.Authors.First(a => a.Id == id);
            return View(author);
        }

        //
        // POST: /Admin/Author/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Author model, HttpPostedFileBase photo, HttpPostedFileBase avatar, FormCollection form)
        {
            try
            {
                ViewBag.Tags = _context.Tags.ToList();
                var author = _context.Authors.First(a => a.Id == id);

                TryUpdateModel(author, new[] { "Name"});

                author.Title = model.Title == null ? "" : HttpUtility.HtmlDecode(model.Title);
                author.TitleEn = model.TitleEn == null ? "" : HttpUtility.HtmlDecode(model.TitleEn);
                author.TitleUa = model.TitleUa == null ? "" : HttpUtility.HtmlDecode(model.TitleUa);

                author.Description = model.Description == null ? "" : HttpUtility.HtmlDecode(model.Description);
                author.DescriptionEn = model.DescriptionEn == null ? "" : HttpUtility.HtmlDecode(model.DescriptionEn);
                author.DescriptionUa = model.DescriptionUa == null ? "" : HttpUtility.HtmlDecode(model.DescriptionUa);

                author.About = model.About == null ? "" : HttpUtility.HtmlDecode(model.About);
                author.AboutEn = model.AboutEn == null ? "" : HttpUtility.HtmlDecode(model.AboutEn);
                author.AboutUa = model.AboutUa == null ? "" : HttpUtility.HtmlDecode(model.AboutUa);

                if (photo != null)
                {
                    if (!string.IsNullOrEmpty(author.Photo))
                    {
                        ImageHelper.DeleteImage(author.Photo, "~/Content/Images/author");
                        ImageHelper.DeleteImage(author.Photo, "~/Content/Images/author/thumb");
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images/author", photo.FileName);
                    string filePath = Server.MapPath("~/Content/Images/author");
                    string filePathThumb = Server.MapPath("~/Content/Images/author/thumb");
                    filePath = Path.Combine(filePath, fileName);
                    filePathThumb = Path.Combine(filePathThumb, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, photo, 670);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, photo, 670, 670, ScaleMode.Crop);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePathThumb, fileName, photo, 324, 324, ScaleMode.Crop);
                    author.Photo = fileName;
                }

                if (avatar != null)
                {
                    if (!string.IsNullOrEmpty(author.Avatar))
                    {
                        ImageHelper.DeleteImage(author.Avatar, "~/Content/Images/author");
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images/author", avatar.FileName);
                    string filePath = Server.MapPath("~/Content/Images/author");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, photo, 670);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, avatar, 150, 150, ScaleMode.Crop);
                    author.Avatar = fileName;
                }


                PostCheckboxesData attrData = form.ProcessPostCheckboxesData("tag");


                author.Tags.Clear();

                foreach (var kvp in attrData)
                {
                    var tagId = kvp.Key;
                    bool tagValue = kvp.Value;

                    if (tagValue)
                    {
                        var tag = _context.Tags.First(t => t.Id == tagId);
                        author.Tags.Add(tag);
                    }
                }

                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var author = _context.Authors.First(e => e.Id == id);
                ImageHelper.DeleteImage(author.Photo, "~/Content/Images/author");
                ImageHelper.DeleteImage(author.Photo, "~/Content/Images/author/thumb");
                ImageHelper.DeleteImage(author.Avatar);

                author.Tags.Clear();

                _context.Authors.Remove(author);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.GetEntityValidationException();

                if (string.IsNullOrEmpty((string)TempData["errorMessage"]))
                {
                    TempData["errorMessage"] = ex.Message;
                }
                return RedirectToAction("Index");
            }

            
        }

    }
}
