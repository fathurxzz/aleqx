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
        public ActionResult Index(string id, int? galleryPage, int? galleryId)
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
                else if (content.Parent != null)
                {
                    var parentContentId = content.Parent.Id;
                    var parentContent = context.Content.Include("Children").Where(pc => pc.Id == parentContentId).First();
                    var subMenuItems = parentContent.Children.OrderBy(c => c.SortOrder).ToList();
                    ViewData["subMenuItems"] = subMenuItems;
                }



                if (content.Galleries.Count > 0)
                {
                    if (galleryPage.HasValue)
                    {
                        ViewData["galleryPage"] = galleryPage.Value;
                    }

                    ViewData["Galleries"] = content.Galleries;

                }
                else
                {
                    ViewData["Galleries"] = new List<Gallery>();
                }

                if(galleryId.HasValue)
                {
                    var gallery = context.Gallery.Include("GalleryItems").Where(g => g.Id == galleryId.Value).First();
                    ViewData["Gallery"] = gallery;
                    ViewData["galleryId"] = galleryId.Value;
                }else
                {
                    ViewData["Gallery"] = new Gallery();
                }


                ViewData["contentName"] = content.Name;
                ViewData["contentId"] = content.Id;
                ViewData["contentLevel"] = content.ContentLevel;



                return View(content);
            }
        }

        public ActionResult SetLanguage(string id)
        {
            SiteSettings.SetCurrentLanguage(id);
            return RedirectToAction("Index", "Home",new{id=""});
        }
    }
}
