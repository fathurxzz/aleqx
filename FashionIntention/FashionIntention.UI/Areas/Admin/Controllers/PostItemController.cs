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
    public class PostItemController : AdminController
    {

        private readonly SiteContext _context;

        public PostItemController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create(int id)
        {
            var post = _context.Posts.First(p => p.Id == id);
            return View(new PostItem{Post = post, PostId = post.Id});
        }

        [HttpPost]
        public ActionResult Create(int postid, PostItem model, HttpPostedFileBase file)
        {
            try
            {
                var post = _context.Posts.First(p => p.Id == postid);
                var postItem = new PostItem
                {
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
                    Post = post
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    //GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 360, 360, ScaleMode.Crop);
                    postItem.ImageSrc = fileName;
                }

                _context.PostItems.Add(postItem);
                _context.SaveChanges();

                return RedirectToAction("Details", "Post", new {id = postid});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var postItem = _context.PostItems.First(p => p.Id == id);
            return View(postItem);
        }

        [HttpPost]
        public ActionResult Edit(int id, PostItem model, HttpPostedFileBase file)
        {
            try
            {
                var postItem = _context.PostItems.First(p => p.Id == id);

                postItem.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
               
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(postItem.ImageSrc))
                    {
                        ImageHelper.DeleteImage(postItem.ImageSrc);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    //GraphicsHelper.SaveOriginalImageWithDefinedDimentions(filePath, fileName, file, 360, 360, ScaleMode.Crop);
                    postItem.ImageSrc = fileName;

                }

                _context.SaveChanges();

                return RedirectToAction("Details", "Post", new { id = postItem.PostId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var postItem = _context.PostItems.First(p => p.Id == id);
            var postId = postItem.PostId;
            if (!string.IsNullOrEmpty(postItem.ImageSrc))
            {
                ImageHelper.DeleteImage(postItem.ImageSrc);
            }
            _context.PostItems.Remove(postItem);
            _context.SaveChanges();
            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
