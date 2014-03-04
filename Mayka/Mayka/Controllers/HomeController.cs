using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mayka.Models;
using SiteExtensions;

namespace Mayka.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index(string id)
        {
            using (var context = new SiteContext())
            {
                var model = new SiteModel(context, id);
                this.SetSeoContent(model);
                return View(model);
            }
        }

    }
}
