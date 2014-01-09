using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AccuV.DataAccess.Entities;
using AccuV.UI.Models;
using AccuVAPI.Operations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Task = System.Threading.Tasks.Task;

namespace AccuV.UI.Controllers.UI
{
    public class AccountController : Controller
    {
        private readonly IActiveDirectoryOperations _activeDirectoryOperations;
        private readonly UserManager<User> _userManager;
        public AccountController(IUserStore<User> userStore, 
            IActiveDirectoryOperations activeDirectoryOperations)
        {
            _activeDirectoryOperations = activeDirectoryOperations;
            _userManager = new UserManager<User>(userStore);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid &&  await _activeDirectoryOperations.ValidateUserAsync(model.UserName, model.Password))
            {
                var user = await _userManager.FindByIdAsync(model.UserName);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
            }

            ModelState.AddModelError("", "Invalid username or password.");

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logoout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
            
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