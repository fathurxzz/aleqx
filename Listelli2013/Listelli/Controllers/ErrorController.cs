﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Listelli.Models;

namespace Listelli.Controllers
{
    public class ErrorController : DefaultController
    {
        public ActionResult Index()
        {
            //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }

        public ActionResult NotFoundPage()
        {
            //Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("404");
        }

    }
}
