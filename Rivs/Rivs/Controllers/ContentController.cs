using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Rivs.Models;

namespace Rivs.Controllers
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
                
                var content = context.Content.Include("Parent").Where(c => c.ContentId == contentId).Select(c => c).FirstOrDefault();
                if (content != null)
                {
                    ViewData["ContentId"] = content.ContentId;
                    if (content.Parent != null)
                    {
                        ViewData["ParentContentId"] = content.Parent.ContentId;
                        ViewData["ParentId"] = content.Parent.Id;
                    }
                }
                return View("Content", content);
            }
        }
    }
}
