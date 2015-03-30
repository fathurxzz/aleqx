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
    public class ArticleController : AdminController
    {
        private readonly SiteContext _context;

        public ArticleController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var posts = _context.Articles.ToList();
            return View(posts);
        }

        //
        // GET: /Admin/Post/Details/5

        public ActionResult Details(int id)
        {
            var post = _context.Articles.First(p => p.Id == id);
            return View(post);
        }

        //
        // GET: /Admin/Post/Create

        public ActionResult Create()
        {
            return View(new Article { Date = DateTime.Now });
        }

        //
        // POST: /Admin/Post/Create

        [HttpPost]
        public ActionResult Create(Post model, FormCollection form)
        {
            try
            {
                var post = new Article
                {
                    Title = model.Title,
                    Date = model.Date,
                    Description = model.Description
                };

                _context.Articles.Add(post);

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
            var post = _context.Articles.First(p => p.Id == id);
            return View(post);
        }

        //
        // POST: /Admin/Post/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Post model, FormCollection form, HttpPostedFileBase file)
        {
            try
            {
                var post = _context.Articles.First(p => p.Id == id);
                TryUpdateModel(post, new[] { "Title", "Date", "Description"});
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
            var post = _context.Articles.First(e => e.Id == id);
            while (post.ArticleItems.Any())
            {
                var pi = post.ArticleItems.First();
                ImageHelper.DeleteImage(pi.ImageSrc);
                _context.ArticleItems.Remove(pi);
            }

            _context.Articles.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
