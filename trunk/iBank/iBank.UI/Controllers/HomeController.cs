using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iBank.SecurityServices;
using iBank.UI.Models;

namespace iBank.UI.Controllers
{
    [Authenticate]
    public class HomeController : Controller
    {
        private string Ip
        {
            get
            {
                //HttpContext.Current.Request.UserHostAddress; 
                //or 
                //HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                //To get the IP address of the machine and not the proxy use the following code

                //HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                return System.Web.HttpContext.Current.Request.UserHostAddress;
                //return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                //return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
        }

        private IAuthenticationService _serviceRepository;

        public HomeController(IAuthenticationService serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }


        public ActionResult Index()
        {
            //var cookie = System.Web.HttpContext.Current.Request.Cookies[SiteSettings.TokenId];
            //if (cookie == null)
            //{
            //    cookie = new HttpCookie(SiteSettings.TokenId);
            //    var authToken = _serviceRepository.GetAuthentificationToken(Ip);
            //    cookie["authToken"] = authToken.AuthentificationTokenValue;
            //    cookie.Expires = authToken.ExpireTime;
            //    cookie.Path = "/";
            //    //System.Web.HttpContext.Current.Response.SetCookie(cookie);
            //    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

            //    return RedirectToAction("LogIn", "Account");
            //}
            

            return View();
        }
    }
}
