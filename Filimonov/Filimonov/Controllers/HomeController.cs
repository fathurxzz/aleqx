using System.Web.Mvc;
using Filimonov.Models;
using SiteExtensions;

namespace Filimonov.Controllers
{
    [HandleError]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteModel(context);
                this.SetSeoContent(model);
                return View(model);
            }
        }

        public ActionResult Projects(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model);
            }
        }

        public ActionResult ProjectDetails(string id)
        {
            using (var context = new SiteContainer())
            {
                var model = new ProjectModel(context, id);
                return View(model.Project);
            }
        }

       
    }
}
