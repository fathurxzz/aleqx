using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Babich.Models;

namespace Babich.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            using (var context = new ContentStorage())
            {
                var menuItems = context.Content.Where(c => c.ContentLevel == 0 && c.Id != 1).OrderBy(c => c.SortOrder).ToList();
                ViewData["menuItems"] = menuItems;

                Content content;
                if (id == null)
                    content = context.Content.Where(c => c.Id == 1).FirstOrDefault();
                else
                {
                    content = context.Content.Include("Parent").Include("Children").Include("Galleries").Where(c => c.Name == id).FirstOrDefault();
                    if (content.Parent != null)
                        ViewData["parentContentName"] = content.Parent.Name;
                }
                if (content.Children.Count > 0)
                {
                    var subMenuItems = content.Children.OrderBy(c => c.SortOrder).ToList();
                    ViewData["subMenuItems"] = subMenuItems;
                }

                ViewData["contentName"] = content.Name;

                return View(content);
            }
        }
    }
}
