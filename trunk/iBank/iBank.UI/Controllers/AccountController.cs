using System;
using System.Web.Mvc;
using iBank.SecurityServices;
using iBank.Api.Exceptions;
using iBank.SecurityServices.Entities;
using iBank.UI.Models;

namespace iBank.UI.Controllers
{
    public class AccountController : ControllerBase
    {

        private string Ip
        {
            get
            {
                return Request.UserHostAddress;
            }
        }

        public AccountController(IAuthenticationService authenticationService):base(authenticationService)
        {

        }

        [Authenticate(true)]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [Authenticate(false, OnlyAuthenticationToken = true)]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cookie = Request.Cookies[SiteSettings.TokenId];
                    if (cookie == null)
                    {
                        throw new SecurityException("cookie not found", SecurityError.Unknow);
                    }

                    var authToken = cookie["authToken"];
                    bool authenticate = _AuthenticationService.Authentification(model.Login, model.Password, Ip, authToken);

                    if (!authenticate)
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(model);
                    }

                    //_repository.GetUser(model.Login, model.Password);
                    //_repository.ValidateUser(model.Login, model.Password);
                    //_authenticationService.GetAuthentificationToken("111");

                    return View("OtpConfirm", new OtpConfirmViewModel ());
                }
                catch (SecurityException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }

        //[Authenticate(OnlyAuthenticationToken = true)]
        //public ActionResult OtpConfirm()
        //{

        //    return View();
        //}

        [HttpPost]
        [Authenticate(false,OnlyAuthenticationToken = true)]
        public ActionResult OtpConfirm(OtpConfirmViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cookie = Request.Cookies[SiteSettings.TokenId];
                    if (cookie == null)
                    {
                        throw new SecurityException("cookie not found", SecurityError.Unknow);
                    }
                    

                    var authToken = cookie["authToken"];
                    SessionToken sessionToken = _AuthenticationService.OtpAuthentificationConfirm(model.OneTimePassword, Ip, authToken);
                    cookie["sessionToken"] = sessionToken.SessionTokenValue;
                    cookie["userName"] = "user_1";
                    cookie.Expires = sessionToken.ExpireTime;
                    cookie.Path = "/";
                    Response.SetCookie(cookie);
                }
                catch (SecurityException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }


        public ActionResult LogOff()
        {
            var cookie = Request.Cookies[SiteSettings.TokenId];
            if (cookie == null)
            {
                throw new SecurityException("cookie not found", SecurityError.Unknow);
            }

            cookie.Expires = DateTime.Now.AddYears(-1);
            cookie.Path = "/";
            Response.SetCookie(cookie);

            return RedirectToAction("LogIn");
        }
    }
}
