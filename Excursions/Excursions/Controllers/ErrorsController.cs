﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Excursions.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/

        public ActionResult NotFound()
        {
            return View();
        }

    }
}
