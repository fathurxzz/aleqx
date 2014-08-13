using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Schema;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class QuickAdviceController : AdminController
    {
        //
        // GET: /Admin/QuickAdvice/

        public QuickAdviceController(IShopRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            _repository.LangId = CurrentLangId;
            var advices = _repository.GetQuickAdvices();
            return View(advices);
        }

        public ActionResult Create()
        {
            _repository.LangId = CurrentLangId;
            return View(new QuickAdvice {CurrentLang = CurrentLangId});
        }

        [HttpPost]
        public ActionResult Create(QuickAdvice model)
        {
            _repository.LangId = CurrentLangId;

            var advice = new QuickAdvice()
            {
                Title = model.Title,
                Active = model.Active,
                SortOrder = model.SortOrder,
            };
            advice.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
            _repository.AddQuickAdvice(advice);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            _repository.LangId = CurrentLangId;
            var advice = _repository.GetQuickAdvice(id);
            return View(advice);
        }

        [HttpPost]
        public ActionResult Edit(QuickAdvice model)
        {
            _repository.LangId = CurrentLangId;

            var advice = _repository.GetQuickAdvice(model.Id);
            TryUpdateModel(advice, new[] {"Title", "Active", "SortOrder"});
            advice.Text = model.Text == null ? "" : HttpUtility.HtmlDecode(model.Text);
            _repository.SaveQuickAdvice(advice);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _repository.LangId = CurrentLangId;
            _repository.DeleteQuickAdvice(id);
            return RedirectToAction("Index");
        }
    }
}
