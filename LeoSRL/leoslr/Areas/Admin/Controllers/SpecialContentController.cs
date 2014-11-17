using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Helpers;
using Leo.Models;
using SiteExtensions;
using SiteExtensions.Graphics;

namespace Leo.Areas.Admin.Controllers
{
    public class SpecialContentController : AdminController
    {
        private readonly SiteContext _context;

        public SpecialContentController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            var content = _context.SpecialContents.ToList();
            foreach (var specialContent in content)
            {
                specialContent.CurrentLang = CurrentLang.Id;
            }
            return View(content);

        }

        //
        // GET: /Admin/SpecialContent/Create

        public ActionResult Create()
        {
            return View(new SpecialContent { CurrentLang = CurrentLang.Id });
        }

        //
        // POST: /Admin/SpecialContent/Create

        [HttpPost]
        public ActionResult Create(SpecialContent model, HttpPostedFileBase pageImage, HttpPostedFileBase contentImage)
        {
            try
            {
                model.Text = HttpUtility.HtmlDecode(model.Text);
                model.Id = 0;
                var cache = new SpecialContent
                {
                    ContentImageSource = "",
                    PageImageSource = "",
                    Title = model.Title ?? "",
                    Text = model.Text ?? "",
                    IsFirstCategory = model.IsFirstCategory,
                    IsSecondCategory = model.IsSecondCategory
                };

                if (pageImage != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", pageImage.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, pageImage);
                    //pageImage.SaveAs(filePath);
                    cache.PageImageSource = fileName;
                }

                if (contentImage != null)
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", contentImage.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, contentImage);
                    contentImage.SaveAs(filePath);
                    cache.ContentImageSource = fileName;
                }

                _context.SpecialContents.Add(cache);

                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var content = _context.SpecialContents.First(c => c.Id == id);
            content.CurrentLang = CurrentLang.Id;
            return View(content);
        }

        //
        // POST: /Admin/SpecialContent/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecialContent model, HttpPostedFileBase pageImage, HttpPostedFileBase contentImage)
        {
            try
            {
                model.Text = HttpUtility.HtmlDecode(model.Text);
                var cache = _context.SpecialContents.First(c => c.Id == model.Id);
                cache.CurrentLang = CurrentLang.Id;
                cache.IsFirstCategory = model.IsFirstCategory;
                cache.IsSecondCategory = model.IsSecondCategory;


                if (pageImage != null)
                {
                    ImageHelper.DeleteImage(cache.PageImageSource);

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", pageImage.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, pageImage);
                    //pageImage.SaveAs(filePath);
                    cache.PageImageSource = fileName;
                }

                if (contentImage != null)
                {
                    ImageHelper.DeleteImage(cache.ContentImageSource);

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", contentImage.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, contentImage);
                    contentImage.SaveAs(filePath);
                    cache.ContentImageSource = fileName;
                }


                var lang = _context.Languages.FirstOrDefault(p => p.Id == model.CurrentLang);
                if (lang != null)
                {
                    CreateOrChangeContentLang(_context, model, cache, lang);
                }


                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {

            var content = _context.SpecialContents.Include("SpecialContentLangs").First(c => c.Id == id);
            ImageHelper.DeleteImage(content.ContentImageSource);
            ImageHelper.DeleteImage(content.PageImageSource);

            while (content.SpecialContentLangs.Any())
            {
                var lang = content.SpecialContentLangs.First();
                _context.SpecialContentLangs.Remove(lang);
            }

            _context.SpecialContents.Remove(content);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }


        private void CreateOrChangeContentLang(SiteContext context, SpecialContent instance, SpecialContent cache, Language lang)
        {

            SpecialContentLang specialContentLang = null;
            if (cache != null)
            {
                specialContentLang = context.SpecialContentLangs.FirstOrDefault(p => p.SpecialContentId == cache.Id && p.LanguageId == lang.Id);
            }
            if (specialContentLang == null)
            {
                var newPostLang = new SpecialContentLang
                {
                    SpecialContentId = instance.Id,
                    LanguageId = lang.Id,
                    Title = instance.Title,
                    Text = instance.Text
                };
                context.SpecialContentLangs.Add(newPostLang);
            }
            else
            {
                specialContentLang.Title = instance.Title;
                specialContentLang.Text = instance.Text;
            }
            context.SaveChanges();

        }
    }
}
