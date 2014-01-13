using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using iBank.DataAccess.Repositories;
using iBank.UI.Models;

namespace iBank.UI.Controllers
{
    [Authenticate]
    public class AccountController : Controller
    {
        //private ISecurityServiceRepository _repository;
        private IUserRepository _repository;

        //public AccountController(ISecurityServiceRepository repository)
        //{
        //    _repository = repository;
        //}

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        
        [AllowAnonymous]
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
                    _repository.GetUser(1);
                    //_repository.ValidateUser(model.Login, model.Password);
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
