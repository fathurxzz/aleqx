using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using iBank.Api.Repositories;
using iBank.DataAccess.EntityFramework;
using iBank.DataAccess.Repositories;
using iBank.UI.Models;

namespace iBank.UI.Controllers
{
    //[Authenticate]
    public class AccountController : Controller
    {
        //private ISecurityServiceRepository _repository;
        private IUserRepository _repository;  // = new UserRepository(new BankStore());

        //public AccountController(ISecurityServiceRepository repository)
        //{
        //    _repository = repository;
        //}

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        
        //[AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.GetUser(model.Login, model.Password);
                    //_repository.ValidateUser(model.Login, model.Password);

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
