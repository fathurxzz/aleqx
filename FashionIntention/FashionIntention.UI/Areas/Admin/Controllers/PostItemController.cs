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
            return View(new PostItem{Post = post});
        }

        //
        // POST: /Admin/PostItem/Create

        [HttpPost]
        public ActionResult Create(int postid, PostItem model, FormCollection collection)
        {
            try
            {
                var post = new PostItem
                {
                    Title = model.Title,
                    Date = model.Date,
                    Description = model.Description,
                    Published = model.Published
                };

                article.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);

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

                return RedirectToAction("Details", "Post", new {id = postid});
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/PostItem/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/PostItem/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/PostItem/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/PostItem/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
