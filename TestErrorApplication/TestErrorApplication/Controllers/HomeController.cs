﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TestErrorApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            //throw new HttpNotFoundException("aaa");
            throw new ArgumentException("aaaa");

            //return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
