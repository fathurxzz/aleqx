using System;
using System.Collections.Generic;
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
            return View(new Author { SortOrder = (_context.Authors.Max(c => (int?)c.SortOrder) ?? 0)+1 });
        }

        //
        // POST: /Admin/Author/Create

        [HttpPost]
        public ActionResult Create(Author model, HttpPostedFileBase photo, HttpPostedFileBase avatar)
        {
            try
            {
                var author = new Author
                {
                    Name = model.Name,
                    Title = model.Title,
                    TitleEn = model.TitleEn,
                    TitleUa = model.TitleUa,
                    Description = model.Description,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionUa = model.DescriptionUa,
                    About = model.About,
                    AboutEn = model.AboutEn,
                    AboutUa = model.AboutUa,
                };

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

                _context.Authors.Add(author);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Author/Edit/5

        public ActionResult Edit(int id)
        {
            var author = _context.Authors.First(a => a.Id == id);
            return View(author);
        }

        //
        // POST: /Admin/Author/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Author model, HttpPostedFileBase photo, HttpPostedFileBase avatar)
        {
            try
            {
                var author = _context.Authors.First(a => a.Id == id);

                TryUpdateModel(author, new[] { "Name", "Title", "TitleEn", "TitleUa", "Description", "DescriptionEn", "DescriptionUa", "About", "AboutEn", "AboutUa", });

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


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var author = _context.Authors.First(e => e.Id == id);
            ImageHelper.DeleteImage(author.Photo, "~/Content/Images/author");
            ImageHelper.DeleteImage(author.Photo, "~/Content/Images/author/thumb");
            ImageHelper.DeleteImage(author.Avatar);

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
