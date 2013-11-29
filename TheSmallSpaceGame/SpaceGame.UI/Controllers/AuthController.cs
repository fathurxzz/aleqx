using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SpaceGame.Api.Contracts.Exceptions;
using SpaceGame.DataAccess.Repositories;
using SpaceGame.UI.Helpers;
using SpaceGame.UI.Models;

namespace SpaceGame.UI.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/

        private IUserRepository _repository;

        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _repository.Register(model.Email, model.Name, model.Password);
                    WebSession.User = user;
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Overview", "Home");
                }
                catch (UserException ex)
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

        public ActionResult LogIn()
        {
            ///////////////////
            //var user = _repository.GetUser("kushko.alex@gmail.com");
            //WebSession.User = user;
            //FormsAuthentication.SetAuthCookie(user.Email, false);
            //return RedirectToAction("Index", "Home");
            ///////////////////

            return View();
        }

        [HttpPost]
        //[HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult LogIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.ValidateUser(model.Email, model.Password);
                    var user = _repository.GetUser(model.Email);
                    WebSession.User = user;
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Overview", "Home");
                }
                catch (UserException ex)
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("LogIn");
        }

    }
}
