using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Klafs.Models;

namespace Klafs.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var contentItems = context.Content.Where(c => c.ContentType == 1).OrderBy(c => c.SortOrder).ToList();
                ViewData["contentItems"] = contentItems;

                var headerMenuItems = context.Content.Where(c => c.ContentType == 2 || c.ContentType == 3).OrderBy(c => c.SortOrder).ToList();
                ViewData["headerMenuItems"] = headerMenuItems;



                

                Content content;
                if (id == null)
                    content = context.Content.Where(c => c.Id == 1).FirstOrDefault();
                else
                {
                    content = context.Content.Include("Parent").Include("Children").Include("GalleryItems").Where(c => c.Name == id).FirstOrDefault();
                    if(content.Parent!=null)
                        ViewData["parentContentName"] = content.Parent.Name;
                }

                if (content.Children.Count > 0)
                {
                    var subMenuItems = content.Children.OrderBy(c => c.SortOrder).ToList();
                    ViewData["subMenuItems"] = subMenuItems;
                }
                else if (content.Parent != null)
                {
                    long parentId = content.Parent.Id;
                    Content parent = context.Content.Include("Children").Where(c => c.Id == parentId).FirstOrDefault();
                    var subMenuItems = parent.Children.OrderBy(c => c.SortOrder).ToList();
                    ViewData["subMenuItems"] = subMenuItems;
                }


                ViewData["contentType"] = content.ContentType;
                ViewData["contentName"] = content.Name;
                ViewData["id"] = content.Id;

                return View(content);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
