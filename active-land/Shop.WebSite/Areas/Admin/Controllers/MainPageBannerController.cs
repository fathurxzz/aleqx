using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Helpers.Graphics;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class MainPageBannerController : AdminController
    {
        //
        // GET: /Admin/MainPageBanner/

        public MainPageBannerController(IShopRepository repository) : base(repository)
        {

        }

        public ActionResult Index()
        {
            return View(_repository.GetMainPageBanners());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MainPageBanner model)
        {
            _repository.LangId = CurrentLangId;
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file == null) continue;
                    if (string.IsNullOrEmpty(file.FileName)) continue;

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    //GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    file.SaveAs(filePath);
                    var mainPageBanner = new MainPageBanner() { ImageSource = fileName };
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


        public ActionResult Delete(int id)
        {
            _repository.DeleteMainPageBanner(id, ImageHelper.DeleteImage);
            return RedirectToAction("Index");
        }

    }
}
