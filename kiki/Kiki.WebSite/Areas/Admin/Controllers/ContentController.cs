using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Areas.Admin.Controllers
{
    public class ContentController : AdminController
    {
        //
        // GET: /Admin/Content/

        public ContentController(ISiteRepository repository) : base(repository)
        {
        }

        public ActionResult Edit()
        {
            return View();
        }

    }
}
