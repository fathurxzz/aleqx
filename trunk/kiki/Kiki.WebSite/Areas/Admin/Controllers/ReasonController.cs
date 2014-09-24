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
    public class ReasonController : AdminController
    {
        public ReasonController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            var articles = _repository.GetReasons();
            return View(articles);
        }

        public ActionResult Create()
        {
            return View(new Reason ());
        }

        [HttpPost]
        public ActionResult Create(Reason model)
        {
            try
            {
                model.Id = 0;
                var article = new Reason
                {
                    Title = model.Title,
                    TitleEng = model.TitleEng,
                    Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text),
                    TextEng = model.TextEng == null ? "" : HttpUtility.HtmlDecode(model.TextEng),
                    SortOrder = model.SortOrder
                };

                _repository.AddReason(article);
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
                var article = _repository.GetReason(id);
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
                var article = _repository.GetReason(model.Id);
                TryUpdateModel(article, new[] { "Title","TitleEng", "SortOrder"});

                article.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
                article.TextEng = model.TextEng == null ? "" : HttpUtility.HtmlDecode(model.TextEng);
                _repository.SaveReason(article);
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
                _repository.DeleteReason(id);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

    }
}
