using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Posh.Helpers;
using Posh.Models;

namespace Posh.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ModelContainer())
            {
                SiteViewModel model = new SiteViewModel(context, id, id);
                this.SetSeoContent(model);
                ViewBag.MainMenu = model.MainMenu;
                ViewBag.isHomePage = model.IsHomePage;

                ViewBag.Categories = model.Categories;
                ViewBag.Elements = model.Elements;

                return View(model);
            }
        }
        
        [OutputCache(VaryByParam = "*", NoStore = true, Duration = 1)]
        public ActionResult ShowFilter()
        {
            using (var context = new ModelContainer())
            {
                SiteViewModel model = new SiteViewModel(context, null, null);
                ViewBag.Categories = model.Categories;
                ViewBag.Elements = model.Elements;
                return PartialView("_SearchCriteriaSelector");
            }
        }

        [HttpPost]
        public PartialViewResult FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            if (ModelState.IsValid)
            {
                MailHelper2.SendTemplate(new List<MailAddress> { new MailAddress("office@posh.net.ua") }, "Форма обратной связи", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);

                return PartialView("Success");
            }
            else
            {
                return PartialView("FeedbackForm", feedbackFormModel);
            }
        }


    }
}
