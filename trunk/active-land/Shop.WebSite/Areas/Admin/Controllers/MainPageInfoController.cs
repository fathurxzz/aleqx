using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;
using Shop.WebSite.Models;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class MainPageInfoController : AdminController
    {
       
        public MainPageInfoController(IShopRepository repository) : base(repository)
        {

        }

        public ActionResult Index()
        {
            var model = new MainPageInfo();

            var avatarImageSource = _repository.GetSiteProperty("AuthorAvatarImageSource");
            if (avatarImageSource != null)
            {
                model.AuthorAvatarImageSource = avatarImageSource.Value;
            }

            var authorGreetingText = _repository.GetSiteProperty("AuthorGreetingText");
            if (authorGreetingText != null)
            {
                model.AuthorGreetingText = authorGreetingText.Value;
            }

            return View(model);
        }

        public ActionResult Edit()
        {
            var model = new MainPageInfo();

            var avatarImageSource = _repository.GetSiteProperty("AuthorAvatarImageSource");
            if (avatarImageSource != null)
            {
                model.AuthorAvatarImageSource = avatarImageSource.Value;
            }

            var authorGreetingText = _repository.GetSiteProperty("AuthorGreetingText");
            if (authorGreetingText != null)
            {
                model.AuthorGreetingText = authorGreetingText.Value;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MainPageInfo model)
        {
            try
            {
                var avatarImageSource = _repository.GetSiteProperty("AuthorAvatarImageSource");
                
                var authorGreetingText = _repository.GetSiteProperty("AuthorGreetingText");
                if (authorGreetingText != null)
                {
                    authorGreetingText.Value = HttpUtility.HtmlDecode(model.AuthorGreetingText);
                }
                _repository.SaveSiteProperty(authorGreetingText);


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


                    if (avatarImageSource != null)
                    {

                        if (!string.IsNullOrEmpty(avatarImageSource.Value))
                        {
                            ImageHelper.DeleteImage(avatarImageSource.Value);
                        }

                        avatarImageSource.Value = fileName;
                    }
                    _repository.SaveSiteProperty(avatarImageSource);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
