using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mayka.Controllers
{
    public class LoginModel
    {
        [Display(Name = "Запомнить")]
        public bool Keep { get; set; }
        [Display(Name = "Логин")]
        public string Name { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }

    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            if(Membership.ValidateUser(loginModel.Name,loginModel.Password))

            //if (FormsAuthentication.Authenticate(loginModel.Name, loginModel.Password))
            {
                FormsAuthentication.SetAuthCookie(loginModel.Name, loginModel.Keep);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View();

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }


}
