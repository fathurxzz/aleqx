using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Helpers.Graphics;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class ServiceController : AdminController
    {
        //
        // GET: /Admin/Service/

        public ServiceController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            var articles = _repository.GetServices();
            return View(articles);
        }

        public ActionResult Create()
        {
            return View(new Service());
        }

        [HttpPost]
        public ActionResult Create(Service model)
        {
            try
            {
                model.Id = 0;
                var service = new Service
                {
                    Title = model.Title,
                    TitleR = model.TitleR,
                    TitleEng = model.TitleEng,
                    Description = model.Description == null ? "" : HttpUtility.HtmlDecode(model.Description),
                    DescriptionEng = model.DescriptionEng == null ? "" : HttpUtility.HtmlDecode(model.DescriptionEng),
                    SortOrder = model.SortOrder,
                    Name = string.IsNullOrEmpty(model.Name)
                        ? SiteHelper.UpdatePageWebName(model.Name, model.Title)
                        : SiteHelper.UpdatePageWebName(model.Name)
                };

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    service.ImageSource = fileName;
                }
                else
                {
                    service.ImageSource = service.ImageSource ?? "";
                }

                _repository.AddService(service);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : "");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var service = _repository.GetService(id);
                return View(service);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Service model)
        {
            try
            {
                var service = _repository.GetService(model.Id);
                service.Name = string.IsNullOrEmpty(model.Name)
                   ? SiteHelper.UpdatePageWebName(model.Name, model.Title)
                   : SiteHelper.UpdatePageWebName(model.Name);
                TryUpdateModel(service, new[] { "Title", "TitleR", "TitleEng", "SortOrder" });

                service.Description = model.Description == null ? "" : HttpUtility.HtmlDecode(model.Description);
                service.DescriptionEng = model.DescriptionEng == null ? "" : HttpUtility.HtmlDecode(model.DescriptionEng);

                var file = Request.Files[0];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    if (!string.IsNullOrEmpty(service.ImageSource))
                    {
                        ImageHelper.DeleteImage(service.ImageSource);
                    }

                    string fileName = IOHelper.GetUniqueFileName("~/Content/Images", file.FileName);
                    string filePath = Server.MapPath("~/Content/Images");

                    filePath = Path.Combine(filePath, fileName);
                    GraphicsHelper.SaveOriginalImage(filePath, fileName, file, 1500);
                    service.ImageSource = fileName;
                }
                else
                {
                    service.ImageSource = service.ImageSource ?? "";
                }
                _repository.SaveService(service);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteService(id, ImageHelper.DeleteImage);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
