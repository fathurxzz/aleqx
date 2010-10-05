using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        //
        // GET: /Admin/Contacts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update()
        {
            using (var context = new ContentStorage())
            {
                var contactsInfo = context.Content.Select(c => c).Where(c => c.IsContactsPage).First();
                return View(contactsInfo);
            }
        }


        [HttpPost]
        public ActionResult Update(Content content)
        {
            using (var context = new ContentStorage())
            {
                var contactsInfo = context.Content.Select(c => c).Where(c => c.IsContactsPage).First();

                content.Id = contactsInfo.Id;
                content.Text = HttpUtility.HtmlDecode(content.Text);
                content.IsContactsPage = true;

                content.ContentId = "Contacts";
                content.Title = "Контакты";

                object originalItem;
                EntityKey entityKey = new EntityKey("ContentStorage.Content", "Id", content.Id);
                if (context.TryGetObjectByKey(entityKey, out originalItem))
                {
                    context.ApplyPropertyChanges(entityKey.EntitySetName, content);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Contacts", new { area = "" });
        }

    }
}
