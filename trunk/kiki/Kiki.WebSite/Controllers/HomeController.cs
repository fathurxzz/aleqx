using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;
using Kiki.WebSite.Helpers;
using Kiki.WebSite.Models;
using ContentType = Kiki.DataAccess.ContentType;

namespace Kiki.WebSite.Controllers
{
    public class HomeController : DefaultController
    {
        public HomeController(ISiteRepository repository) : base(repository)
        {

        }

        public ActionResult Index(string id)
        {
            var model = new SiteModel(_repository, id);
            this.SetSeoContent(model);
            switch ((ContentType)model.Content.ContentType)
            {
                case ContentType.Gallery:
                    return View("Gallery", model);
                default:
                    return View(model);
            }
        }

    }
}
