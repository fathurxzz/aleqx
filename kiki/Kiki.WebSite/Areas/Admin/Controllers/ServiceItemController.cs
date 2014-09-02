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
    public class ServiceItemController : AdminController
    {
        public ServiceItemController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Create(int id)
        {
            return View(new ServiceItem{ServiceId = id});
        }

        [HttpPost]
        public ActionResult Create(ServiceItem model)
        {
            try
            {
                var service = _repository.GetService(model.ServiceId);
                var serviceItem = new ServiceItem
                {
                    Service = service,
                    Title = model.Title,
                    Description = model.Description,
                    SortOrder = model.SortOrder,
                    Price = model.Price
                };
                _repository.AddServiceItem(serviceItem);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message + (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) ? ex.InnerException.Message : "");
                return View(model);
            }

            return RedirectToAction("Index", "Service");
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var service = _repository.GetServiceItem(id);
                return View(service);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index", "Service");
            }
        }

        [HttpPost]
        public ActionResult Edit(ServiceItem model)
        {
            try
            {
                var service = _repository.GetServiceItem(model.Id);
                TryUpdateModel(service, new[] { "Title", "Description", "SortOrder" ,"Price"});
                _repository.SaveServiceItem(service);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index","Service");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteServiceItem(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index", "Service");
        }
    }
}
