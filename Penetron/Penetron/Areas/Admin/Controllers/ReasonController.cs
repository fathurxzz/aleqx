using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Penetron.Models;

namespace Penetron.Areas.Admin.Controllers
{
    [Authorize]
    public class ReasonController : Controller
    {
        private SiteContext _context;
        public ReasonController(SiteContext context)
        {
            _context = context;
        }

        public ActionResult Edit(int id)
        {
            var reason = _context.Reason.First(r => r.Id == id);
            
            return View(reason);
        }

        [HttpPost]
        public ActionResult Edit(Reason model)
        {
            var reason = _context.Reason.First(r => r.Id == model.Id);
            reason.Title = model.Title;
            reason.Text = HttpUtility.HtmlDecode(model.Text);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home", new {area = ""});
        }

    }
}
