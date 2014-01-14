using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using iBank.SecurityServices;
using iBank.UI.Models;

namespace iBank.UI.Controllers
{
    [Authenticate]
    public class AccountController : Controller
    {
        private IAuthenticationService _serviceRepository; 

        public AccountController(IAuthenticationService serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [Authenticate(AllowAnonymus = true)]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [Authenticate(AuthenticateAction = true)]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_repository.GetUser(model.Login, model.Password);
                    //_repository.ValidateUser(model.Login, model.Password);
                    //_serviceRepository.GetAuthentificationToken("111");
                    return RedirectToAction("Index", "Home");
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
    }
}
