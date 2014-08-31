using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Entities;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class SubscriberController : AdminController
    {
        //
        // GET: /Admin/Subscriber/

        public SubscriberController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            var articles = _repository.GetSubscribers();
            return View(articles);
        }

        public ActionResult Create()
        {
            return View(new Subscriber());
        }

        [HttpPost]
        public ActionResult Create(Subscriber model)
        {
            try
            {
                model.Id = 0;
                var article = new Subscriber
                {
                   Email = model.Email
                };

                _repository.AddSubscriber(article);
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
                var article = _repository.GetSubscriber(id);
                return View(article);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Article model)
        {
            try
            {
                var article = _repository.GetSubscriber(model.Id);
                TryUpdateModel(article, new[] { "Email" });

                _repository.SaveSubscriber(article);
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
                _repository.DeleteSubscriber(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }


    }
}
