using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionIntention.UI.Models;

namespace FashionIntention.UI.Areas.Admin.Controllers
{
    public class TagController : AdminController
    {
        private readonly SiteContext _context;

        public TagController(SiteContext context)
        {
            _context = context;
        }

       
        public ActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }

    
        public ActionResult Create()
        {
            return View(new Tag());
        }

       
        [HttpPost]
        public ActionResult Create(Tag model)
        {
            try
            {
                var tag = new Tag
                {
                    Title = model.Title
                };
                _context.Tags.Add(tag);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Edit(int id)
        {
            var tag = _context.Tags.First(t => t.Id == id);
            return View(tag);
        }

      
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var tag = _context.Tags.First(t => t.Id == id);
                TryUpdateModel(tag, new[] { "Title" });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Delete(int id)
        {
            var tag = _context.Tags.First(t => t.Id == id);
            
            tag.Posts.Clear();
            tag.Posts = null;

            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
