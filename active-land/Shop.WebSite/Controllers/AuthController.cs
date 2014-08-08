using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shop.WebSite.Controllers
{
    public class LoginModel
    {
        [Display(Name = "Запомнить")]
        public bool Keep { get; set; }
        [Display(Name = "Логин")]
        public string Name { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { lang = "ru" });
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            //if (Membership.ValidateUser(loginModel.Name, loginModel.Password))
            if (loginModel.Name == "admin" && loginModel.Password == "active2014")
            {
                FormsAuthentication.SetAuthCookie(loginModel.Name, loginModel.Keep);
                return RedirectToLocal(returnUrl);
            }
            loginModel.ErrorMessage = "Неправильные логин или пароль";
            return View(loginModel);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Category", new { area = "Admin", lang = "ru" });
            }
        }

    }
}
