using System.Linq;
using System.Web.Mvc;
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
                ViewData["mcontentId"] = contentId;
                var content = context.Content.Include("Parent").Include("Galleries").Include("Children").Where(c => c.ContentId == contentId).FirstOrDefault();
                if (content != null)
                {
                    if (content.Parent != null)
                    {
                        ViewData["parentContentId"] = content.Parent.ContentId;
                        ViewData["mparentContentId"] = content.Parent.ContentId;
                    }



                    if (content.Horisontal && content.HorisontalLevel > 0)
                    {
                        var mcontent = context.Content.Include("Parent").Include("Galleries").Include("Children").Where(c => c.Horisontal&&c.HorisontalLevel==0).FirstOrDefault();
                        if(mcontent!=null)
                        {
                            ViewData["mcontentId"] = mcontent.ContentId;
                            if (mcontent.Parent != null)
                            {
                                ViewData["mparentContentId"] = mcontent.Parent.ContentId;
                            }

                        }
                    }
                }


                

                return View("Content", content);
            }
        }

        public ActionResult HorisontalMenuLine(string contentId, int level, bool itemHasChildren, string parentParentContentId, string currentContentId)
        {
            using (var context = new ContentStorage())
            {
                var parentContentId = context.Content.Where(c => c.ContentId == contentId).Select(c => c.Parent.ContentId).FirstOrDefault();
                var menuItemsList = context.Content.Where(c => c.ContentId == contentId).SelectMany(c => c.Children).Where(c => c.Horisontal).ToList();

                if (menuItemsList.Count == 0 && level == 0)
                {
                    ViewData["lastLevel"] = true;
                }
                else
                {
                    ViewData["lastLevel"] = false;
                }

                ViewData["level"] = level + 1;
                ViewData["contentId"] = contentId;
                ViewData["parentContentId"] = parentContentId;
                ViewData["parentParentContentId"] = parentParentContentId;
                ViewData["currentContentId"] = currentContentId;
                ViewData["itemHasChildrenParent"] = menuItemsList.Count > 0;
                ViewData["itemHasChildren"] = itemHasChildren;

                return View(menuItemsList);
            }
        }
    }
}
