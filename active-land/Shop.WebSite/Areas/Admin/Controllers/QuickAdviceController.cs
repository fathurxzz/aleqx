using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View(new QuickAdvice {CurrentLang = CurrentLangId});
        }

        public ActionResult Create(QuickAdvice model)
        {

        }
    }
}
