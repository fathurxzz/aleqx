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
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/

        [HttpGet]
        public ActionResult AddEdit(int? id)
        {
            Content content;
            if (id.HasValue)
            {
                using (var context = new ContentStorage())
                {
                    content = context.Content.Select(c => c).Where(c => c.Id == id).First();
                }
            }
            else
            {
                content = new Content();
            }
            return View(content);
        }

        [HttpPost]
        public ActionResult AddEdit(Content content, int? Id)
        {

            if (String.IsNullOrEmpty(content.Title))
                ModelState.AddModelError("Title", "Title is required!");
            if (String.IsNullOrEmpty(content.Text))
                ModelState.AddModelError("Text", "Text is required!");
            if (String.IsNullOrEmpty(content.ContentId))
                ModelState.AddModelError("ContentId", "ContentId is required!");


            content.Text = HttpUtility.HtmlDecode(content.Text);

            if (ModelState.IsValid)
            {
                
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
                return RedirectToAction("Index", "Content", new {area = "", id = content.ContentId});
            }
            return View(content);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var context = new ContentStorage())
            {
                var content = context.Content.Select(c => c).Where(c => c.Id == id).First();
                context.DeleteObject(content);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Excursions", new { area = "" });
        }

    }
}
