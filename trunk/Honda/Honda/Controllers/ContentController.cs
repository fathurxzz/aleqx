using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Honda.Models;

namespace Honda.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public ActionResult Index(string id)
        {
            string contentId = id;

            using (var context = new ContentStorage())
            {
                ViewData["contentId"] = contentId;
                var content = context.Content.Include("Parent").Include("Galleries").Include("Children").Where(c => c.ContentId == contentId).FirstOrDefault();
                if (content != null)
                {
                    if (content.Parent != null)
                    {
                        ViewData["parentContentId"] = content.Parent.ContentId;

                        if(!content.Horisontal)
                        {
                            var childrenItems = context.Content.Where(c => c.ContentId == contentId).SelectMany(c => c.Children).ToList();
                            if (childrenItems.Count > 0)
                            {
                                return RedirectToAction("Index", "Content", new {id = childrenItems[0].ContentId});
                            }
                        }
                    }
                }

                return View("Content", content);
            }
        }

    }
}
