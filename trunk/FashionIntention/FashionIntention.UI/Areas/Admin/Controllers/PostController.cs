using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Helpers;
using FashionIntention.UI.Models;

namespace FashionIntention.UI.Areas.Admin.Controllers
{
    public class PostController : AdminController
    {
        private readonly SiteContext _context;

        public PostController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        //
        // GET: /Admin/Post/Details/5

        public ActionResult Details(int id)
        {
            var post = _context.Posts.First(p => p.Id == id);
            return View(post);
        }

        //
        // GET: /Admin/Post/Create

        public ActionResult Create()
        {
            return View(new Post{Date = DateTime.Now});
        }

        //
        // POST: /Admin/Post/Create

        [HttpPost]
        public ActionResult Create(Post model, HttpPostedFileBase file)
        {
            try
            {
                var post = new Post
                {
                    Title = model.Title,
                    Date = model.Date,
                    Description = model.Description,
                    Published = model.Published
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 360, 360, ScaleMode.Crop);
                    post.ImageSrc = fileName;
                }

                _context.Posts.Add(post);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Post/Edit/5

        public ActionResult Edit(int id)
        {
            var post = _context.Posts.First(p => p.Id == id);
            return View(post);
        }

        //
        // POST: /Admin/Post/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Post model, HttpPostedFileBase file)
        {
            try
            {
                var post = _context.Posts.First(p => p.Id == id);
                TryUpdateModel(post, new[] { "Title", "Date", "Description", "Published" });

                if (file != null)
                {
                    if (!string.IsNullOrEmpty(post.ImageSrc))
                    {
                        ImageHelper.DeleteImage(post.ImageSrc);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 360, 360, ScaleMode.Crop);
                    post.ImageSrc = fileName;

                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Post/Delete/5

        public ActionResult Delete(int id)
        {
            var post = _context.Posts.First(e => e.Id == id);
            ImageHelper.DeleteImage(post.ImageSrc);

            //foreach (var image in article.ArticleImages)
            //{
            //    ImageHelper.DeleteImage(image.ImageSrc);
            //}

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
