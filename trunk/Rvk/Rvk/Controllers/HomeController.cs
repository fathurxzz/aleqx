using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Rvk.Helpers;
using Rvk.Models;

namespace Rvk.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeedbackForm()
        {
            return PartialView("FeedbackForm");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public void FeedbackForm(FeedbackFormModel feedbackFormModel)
        {
            //MailHelper.SendTemplate(new List<MailAddress> { new MailAddress("miller.kak.miller@gmail.com") }, "Форма обратной связи RVK", "FeedbackTemplate.htm", null, true, feedbackFormModel.Name, feedbackFormModel.Email, feedbackFormModel.Text);
            
            //Response.Write("<script>window.top.$.fancybox.close()</script>");
            
            //Response.Write("<script>window.top.location.href=window.top.location.href</script>");
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public void Subscribe()
        {

        }
    }
}
