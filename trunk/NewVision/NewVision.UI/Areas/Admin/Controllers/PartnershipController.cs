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
    public class PartnershipController : Controller
    {
        private readonly SiteContext _context;

        public PartnershipController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var contents = _context.Partnerships.ToList();
            return View(contents);
        }

        //
        // GET: /Admin/Partnership/Create

        public ActionResult Create()
        {
            return View(new Partnership());
        }

        //
        // POST: /Admin/Partnership/Create

        [HttpPost]
        public ActionResult Create(Partnership model, HttpPostedFileBase file)
        {
            try
            {
                var content = new Partnership
                {
                    Title = model.Title,
                    Text = model.Text
                };

                if (file != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    content.ImageSrc = fileName;
                }
                _context.Partnerships.Add(content);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Partnership/Edit/5

        public ActionResult Edit(int id)
        {
            var article = _context.Partnerships.First(e => e.Id == id);
            return View(article);
        }

        //
        // POST: /Admin/Partnership/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase file)
        {
            try
            {
                var article = _context.Partnerships.First(e => e.Id == id);
                TryUpdateModel(article, new[] {"Title", "Text"});
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(article.ImageSrc))
                    {
                        ImageHelper.DeleteImage(article.ImageSrc);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    article.ImageSrc = fileName;
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
        // GET: /Admin/Partnership/Delete/5

        public ActionResult Delete(int id)
        {
            var article = _context.Partnerships.First(e => e.Id == id);
            ImageHelper.DeleteImage(article.ImageSrc);
            _context.Partnerships.Remove(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
