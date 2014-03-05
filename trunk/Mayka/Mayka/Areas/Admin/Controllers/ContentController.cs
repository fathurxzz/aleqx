using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Models;

namespace Mayka.Areas.Admin.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly SiteContext _context;

        public ContentController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content model)
        {
            var content = new Content();
            TryUpdateModel(content, new[] {
                    "Title",
                    "Name",
                    "MenuTitle",
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "ContentType"
                });
            content.Text = HttpUtility.HtmlDecode(model.Text);

            _context.Content.Add(content);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
        }

        public ActionResult Edit(int id)
        {
            return View(_context.Content.First(c => c.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(Content model)
        {
            var content = _context.Content.First(c => c.Id == model.Id);
            TryUpdateModel(content, new[]
                {
                    "Title",
                    "Name",
                    "MenuTitle",
                    "SortOrder",
                    "SeoDescription",
                    "SeoKeywords",
                    "ContentType"
                });

            content.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "", id = content.Name });
        }

        public ActionResult Delete(int id)
        {
            var content = _context.Content.First(c => c.Id == id);
            _context.Content.Remove(content);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
