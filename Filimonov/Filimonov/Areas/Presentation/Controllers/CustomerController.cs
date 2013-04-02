using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Filimonov.Models;

namespace Filimonov.Areas.Presentation.Controllers
{
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
    public class CustomerController : Controller
    {
        //
        // GET: /Presentation/Customer/

        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            using (var context = new LibraryContainer())
            {

                var customers = context.Customer.Include("ProductSets").ToList();

                //string[] users = Roles.GetUsersInRole("Customers");


                return View(customers);
            }

        }

        //
        // GET: /Presentation/Customer/Details/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Presentation/Customer/Create
        [Authorize(Roles = "Administrators")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Presentation/Customer/Create

        [Authorize(Roles = "Administrators")]
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
                        if (!Roles.IsUserInRole(model.UserName, "Customers"))
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

        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int id)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.First(c => c.Id == id);
                return View(customer);
            }
        }

        //
        // POST: /Presentation/Customer/Edit/5

        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var context = new LibraryContainer())
                {
                    var customer = context.Customer.First(c => c.Id == id);
                    TryUpdateModel(customer, new[] { "Title" });
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Presentation/Customer/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int id)
        {
            using (var context = new LibraryContainer())
            {
                var customer = context.Customer.Include("ProductSets").First(c => c.Id == id);
                string userName = customer.Name;

                while (customer.ProductSets.Any())
                {
                    var productContainer = customer.ProductSets.First();
                    context.DeleteObject(productContainer);
                    context.SaveChanges();
                }

                context.DeleteObject(customer);

                Membership.DeleteUser(userName);

                context.SaveChanges();

                return RedirectToAction("Index");
            }

        }

        public ActionResult LogOn(string id)
        {
            if (Membership.ValidateUser(id, "cde32wsx") && id != "admin")
            {
                FormsAuthentication.SetAuthCookie(id, true);
            }
            return RedirectToAction("Index", "Home", new { area = "Presentation" });
        }
    }
}
