using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iBank.UI.Models;

namespace iBank.UI.Controllers
{
    //[Authenticate]
    public class HomeController : Controller
    {


        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Response.SetCookie(new HttpCookie("aaa"));

            return View();
        }
    }
}
