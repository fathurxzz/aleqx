using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ViaCon.Models;

namespace ViaCon.Controllers
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
                var content = context.Content.Include("Parent")/*.Include("Galleries")*/.Include("Children").Where(c => c.ContentId == contentId).FirstOrDefault();
                if (content != null)
                {
                    if (content.Parent != null)
                    {
                        ViewData["parentContentId"] = content.Parent.ContentId;
                    }
                }
                else
                {
                    //throw new HttpException(404, "Приехали");
                }
                return View("Content", content);
            }
        }

    }
}
