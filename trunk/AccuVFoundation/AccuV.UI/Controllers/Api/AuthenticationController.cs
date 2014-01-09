using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AccuV.DataAccess.Entities;
using AccuV.UI.Models;
using AccuVAPI.Operations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Task = System.Threading.Tasks.Task;

namespace AccuV.UI.Controllers.Api
{
    public class AuthenticationController : ApiController
    {
        private readonly UserManager<User> _userManager;

        private readonly IActiveDirectoryOperations _activeDirectoryOperations;

        public AuthenticationController(IUserStore<User> userStore,
            IActiveDirectoryOperations activeDirectoryOperations)
        {
            _userManager = new UserManager<User>(userStore);
            _activeDirectoryOperations = activeDirectoryOperations;
        }

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post(LoginViewModel model)
        {
            if (ModelState.IsValid && await _activeDirectoryOperations.ValidateUserAsync(model.UserName, model.Password))
            {
                var user = await _userManager.FindByIdAsync(model.UserName);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }

            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
    }
}
