﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mayka.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/

        public ActionResult Edit()
        {
            return View();
        }

    }
}