using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excursions.Models;

namespace Excursions.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            Content content = null;
            if (id.HasValue)
            {
                using (var context = new ContentStorage())
                {
                    content = context.Content.Select(c => c).Where(c => c.Id == id).First();
                }
            }
            return View(content);
        }

        [HttpPost]
        public ActionResult AddEdit(Content content, int? Id)
        {
            content.Text = HttpUtility.HtmlDecode(content.Text);
            using (var context = new ContentStorage())
            {
                if (Id.HasValue && Id.Value > 0)
                {
                    content.Id = Id.Value;
                    object originalItem;
                    EntityKey entityKey = new EntityKey("ContentStorage.Content", "Id", content.Id);
                    if (context.TryGetObjectByKey(entityKey, out originalItem))
                    {
                        context.ApplyPropertyChanges(entityKey.EntitySetName, content);
                    }
                }
                else
                {
                    context.AddToContent(content);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Content", new { area = "" });
        }

    }
}
