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
            ViewBag.Tags = _context.Tags.ToList();
            return View(new Post{Date = DateTime.Now});
        }

        //
        // POST: /Admin/Post/Create

        [HttpPost]
        public ActionResult Create(Post model, FormCollection form, HttpPostedFileBase file)
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

                PostCheckboxesData tags = form.ProcessPostCheckboxesData("tag");
                foreach (var kvp in tags)
                {
                    var tagId = kvp.Key;
                    bool ischecked = kvp.Value;

                    if (ischecked)
                    {
                        var tag = _context.Tags.First(t => t.Id == tagId);
                        post.Tags.Add(tag);
                    }
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
            ViewBag.Tags = _context.Tags.ToList();
            var post = _context.Posts.First(p => p.Id == id);
            return View(post);
        }

        //
        // POST: /Admin/Post/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Post model, FormCollection form, HttpPostedFileBase file)
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

                PostCheckboxesData tags = form.ProcessPostCheckboxesData("tag");
                foreach (var kvp in tags)
                {
                    var tagId = kvp.Key;
                    bool ischecked = kvp.Value;
                    var tag = _context.Tags.First(t => t.Id == tagId);

                    if (ischecked)
                    {
                        post.Tags.Add(tag);
                    }
                    else
                    {
                        if (post.Tags.Contains(tag))
                        {
                            post.Tags.Remove(tag);
                        }
                    }
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

            post.Tags.Clear();
            post.Tags = null;

            //foreach (var image in article.ArticleImages)
            //{
            //    ImageHelper.DeleteImage(image.ImageSrc);
            //}

            while (post.PostItems.Any())
            {
                var pi = post.PostItems.First();
                ImageHelper.DeleteImage(pi.ImageSrc);
                _context.PostItems.Remove(pi);
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
