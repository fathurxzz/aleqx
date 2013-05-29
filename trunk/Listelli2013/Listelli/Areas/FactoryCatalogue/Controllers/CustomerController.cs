using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Listelli.App_LocalResources;
using Listelli.Controllers;
using Listelli.Models;

namespace Listelli.Areas.FactoryCatalogue.Controllers
{
    public class CustomerController : DefaultController
    {

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Category",new{area="FactoryCatalogue"});
        }


        public ActionResult LogOn()
        {
            ViewBag.CurrentMenuItem = "factory";
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(CustomerLogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    return RedirectToAction("Index", "Category", new { area = "FactoryCatalogue" });
                }
                else
                {
                    ModelState.AddModelError("", GlobalRes.WrongLoginOrPassword);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        public ActionResult Register()
        {
            ViewBag.CurrentMenuItem = "factory";
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string email = model.UserName + "@domain.com";
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.Email, model.Password, model.UserName, null, null, true, null, out createStatus);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        if (!Roles.RoleExists("Customers"))
                            Roles.CreateRole("Customers");
                        if (!Roles.IsUserInRole(model.Email, "Customers"))
                            Roles.AddUserToRole(model.Email, "Customers");


                        FormsAuthentication.SetAuthCookie(model.Email, false /* createPersistentCookie */);
                        //using (var context = new LibraryContainer())
                        //{
                        //    var customer = new Customer { Name = model.UserName, Title = model.UserTitle};
                        //    context.AddToCustomer(customer);
                        //    context.SaveChanges();
                        //}

                        return RedirectToAction("Index", "Category", new { area = "FactoryCatalogue" });
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
