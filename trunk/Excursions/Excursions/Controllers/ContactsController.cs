using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;
using Excursions.Models.Captcha;

namespace Excursions.Controllers
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/

        public ActionResult Index()
        {
            using (var context = new ContentStorage())
            {
                var contactsInfo = context.Content.Select(c => c).Where(c => c.IsContactsPage).First();
                return View(contactsInfo);
            }
        }


        [HttpPost]
        [CaptchaValidation("captcha")]
        public ActionResult FeedBack(FormCollection form)
        {
            
            return RedirectToAction("Index", "Contacts");
        }

    }
}
