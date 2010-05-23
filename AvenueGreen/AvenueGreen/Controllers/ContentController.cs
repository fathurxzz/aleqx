using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AvenueGreen.Models;

namespace AvenueGreen.Controllers
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
                var content = context.Content.Include("Parent").Include("Children").Where(c => c.ContentId == contentId).FirstOrDefault();
                if (content != null)
                {
                    ViewData["contentLevel"] = content.ContentLevel;
                    if (content.Parent != null)
                    {
                        ViewData["parentContentId"] = content.Parent.ContentId;
                        ViewData["parentId"] = content.Parent.Id;
                        var pcontent = context.Content.Include("Parent").Where(c => c.ContentId == content.Parent.ContentId).FirstOrDefault();
                        if (pcontent.Parent != null)
                        {
                            ViewData["parentParentContentId"] = pcontent.Parent.ContentId;
                            ViewData["parentId"] = pcontent.Parent.Id;
                        }
                    }
                    else
                    {
                        ViewData["parentId"] = content.Id;
                    }
                }
                return View("Content", content);
            }
        }

    }
}
