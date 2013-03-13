using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class CustomerController : Controller
    {
        //
        // GET: /Presentation/Customer/

        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {

                var customers = context.Customer.ToList();

                string[] users = Roles.GetUsersInRole("Customers");


                return View(customers);
            }

        }

        //
        // GET: /Presentation/Customer/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Presentation/Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Presentation/Customer/Create

        [HttpPost]
        public ActionResult Create(RegisterNewCustomerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string email = model.UserName + "@domain.com";
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.UserName, "cde32wsx", email, null, null, true, null, out createStatus);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        if (!Roles.RoleExists("Customers"))
                            Roles.CreateRole("Customers");
                        Roles.AddUserToRole(model.UserName, "Customers");

                        using (var context = new LibraryContainer())
                        {
                            var customer = new Customer { Name = model.UserName, Title = model.UserTitle };
                            context.AddToCustomer(customer);
                            context.SaveChanges();
                        }

                        //FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                        return RedirectToAction("Index", "Customer", new { area = "Presentation" });
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

        //
        // GET: /Presentation/Customer/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Presentation/Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Presentation/Customer/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
