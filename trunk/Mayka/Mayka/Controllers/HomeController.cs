using System.Web.Mvc;
using Mayka.Helpers;
using Mayka.Models;
using SiteExtensions;

namespace Mayka.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SiteContext _context;

        public HomeController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Index(string id)
        {
            var model = new SiteModel(_context, id);
            ViewBag.isHomePage = model.Content.ContentType == (int)ContentType.HomePage;
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Gallery(string id)
        {
            var model = new SiteModel(_context, id);
            this.SetSeoContent(model);
            return View(model);
        }

        public ActionResult Products(string id)
        {
            var model = new SiteModel(_context, id);
            this.SetSeoContent(model);
            return View(model);
        }
    }
}
