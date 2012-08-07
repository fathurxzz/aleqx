using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leo.Models;
using SiteExtensions;

namespace Leo.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            using (var context = new SiteContainer())
            {
                var model = new SiteViewModel(context, null);
                this.SetSeoContent(model);
                return View(model);
            }
        }

    }
}
