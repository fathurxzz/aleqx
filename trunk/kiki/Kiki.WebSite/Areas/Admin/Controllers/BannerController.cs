﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Helpers.Graphics;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class BannerController : AdminController
    {
        //
        // GET: /Admin/Banner/

        public BannerController(ISiteRepository repository) : base(repository)
        {

        }

        public ActionResult Index()
        {
            var siteImages = _repository.GetSiteImages(ImageType.Banner);
            return View(siteImages);
        }

        public ActionResult Create()
        {
            return View(new SiteImage { ImageType = (int)ImageType.Banner });
        }

        [HttpPost]
        public ActionResult Create(SiteImage model)
        {
            try
            {
                model.Id = 0;
                var siteImage = new SiteImage
                {
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
                    ImageType = model.ImageType
                };

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    siteImage.ImageSource = fileName;
                }
                else
                {
                    siteImage.ImageSource = siteImage.ImageSource ?? "";
                }

                _repository.AddSiteImage(siteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : "");
                return View(model);
            }

            return RedirectToAction("Index");
        }

 

        

        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteSiteImage(id, ImageHelper.DeleteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
