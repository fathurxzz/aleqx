using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;

namespace bigs.Controllers
{
    public class BaseContentController : Controller
    {
        //
        // GET: /BaseContent/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string contentUrl = filterContext.RouteData.Values["contentUrl"].ToString();

            if (contentUrl != null)
            {
                
                using (bigs.Models.DataStorage context = new bigs.Models.DataStorage())
                {
                    SiteContent content = Utils.GetContent(contentUrl);
                    
                    if (content.Language != SystemSettings.CurrentLanguage)
                    {
                        SystemSettings.CurrentLanguage = content.Language;
                    }
                    ViewData["text"] = content.Text;
                    ViewData["contentUrl"] = contentUrl;
                    ViewData["text"] = content.Text;
                    ViewData["title"] = content.Title;
                    ViewData["keywords"] = content.Keywords;
                    ViewData["description"] = content.Description;
                    ViewData["contentName"] = content.Name;
                }
            }



            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index(string contentUrl)
        {
            
            

            return View();
        }
    }
}
