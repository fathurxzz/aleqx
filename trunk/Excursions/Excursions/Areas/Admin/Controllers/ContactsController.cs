using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
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
                object originalItem;
                content.Text = HttpUtility.HtmlDecode(content.Text);
                EntityKey entityKey = new EntityKey("ContentStorage.Content", "Id", contactsInfo.Id);
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
