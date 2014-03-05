using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mayka.Areas.Admin.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        //
        // GET: /Admin/Gallery/

        public ActionResult Edit()
        {
            return View();
        }

    }
}
