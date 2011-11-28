using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nebo.Models;
using Nebo.Helpers;
using System.Net.Mail;

namespace Nebo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var menuList = Menu.GetMenuList(id, context);
                ViewBag.MenuList = menuList;

                var content = context.Content
                    .Include("Parent").Include("Children")
                    .Where(c => c.Name == id)
                    .FirstOrDefault();

                if (content == null)
                    content = context.Content
                        .Include("Parent").Include("Children")
                        .Where(c => c.ContentLevel == 0)
                        .First();

                ViewBag.Title = content.Title;
                ViewBag.SeoDescription = content.SeoDescription;
                ViewBag.SeoKeywords = content.SeoKeywords;

                return View(content);
            }
        }



        [HttpPost]
        public PartialViewResult FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            if (ModelState.IsValid)
            {
                MailHelper.SendTemplate(new List<MailAddress> { new MailAddress("miller.kak.miller@gmail.com") }, "Форма обратной связи", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);

                return PartialView("Success");
            }
            else
            {
                return PartialView("FeedbackForm", feedbackFormModel);
            }
        }
    }
}
