using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Excursions.Helpers;
using Excursions.Models;

namespace Excursions.Controllers
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/

        public ActionResult Index(string message)
        {
            using (var context = new ContentStorage())
            {
                var contactsInfo = context.Content.Select(c => c).Where(c => c.IsContactsPage).First();
                ViewData["message"] = message;
                return View(contactsInfo);
            }
        }
    }
}
