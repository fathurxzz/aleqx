using System.Web.Mvc;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(CurrentLang, context, id);
                ViewBag.IsHomePage = model.IsHomePage;
                this.SetSeoContent(model);
                ViewBag.CurrentMenuItem = model.Content.Name;
                return View(model);
            }
        }

        public ActionResult Articles()
        {
            using (var context = new SiteContainer())
            {
                var model = new ArticlesModel(CurrentLang, context);
                ViewBag.CurrentMenuItem = "news";
                return View(model);
            }
        }
    }
}
