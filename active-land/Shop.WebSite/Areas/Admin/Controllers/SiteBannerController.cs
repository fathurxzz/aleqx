using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class SiteBannerController : AdminController
    {
        public SiteBannerController(IShopRepository repository)
            : base(repository)
        {

        }

        public ActionResult Index()
        {
            return View(_repository.GetSiteBanners());
        }

        public ActionResult Create()
        {
            return View(new MainPageBanner());
        }

        [HttpPost]
        public ActionResult Create(MainPageBanner model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                

                var file = Request.Files[0];

                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");
                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    file.SaveAs(filePath);
                    
                    var mainPageBanner = new MainPageBanner {IsSiteBanner = true, ImageSource = fileName, Url = model.Url};
                    _repository.AddMainPageBanner(mainPageBanner);
                }


            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            var banner = _repository.GetMainPageBanner(id);
            return View(banner);
        }

        [HttpPost]
        public ActionResult Edit(MainPageBanner model)
        {
            _repository.LangId = CurrentLangId;
            var banner = _repository.GetMainPageBanner(model.Id);

            var file = Request.Files[0];
            if (file != null && !string.IsNullOrEmpty(file.FileName))
            {
                if (!string.IsNullOrEmpty(banner.ImageSource))
                {
                    ImageHelper.DeleteImage(banner.ImageSource);
                }

                string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                string filePath = Server.MapPath("~/Content/Images");

                filePath = Path.Combine(filePath, fileName);
                //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                file.SaveAs(filePath);
                banner.ImageSource = fileName;
            }

            banner.Url = model.Url;
            _repository.SaveMainPageBanner(banner);


            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            _repository.DeleteMainPageBanner(id, ImageHelper.DeleteImage);
            return RedirectToAction("Index");
        }

    }
}
