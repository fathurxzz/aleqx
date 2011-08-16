using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;

namespace Exchange.Controllers
{
    public class AccountsController : Controller
    {
        //
        // GET: /Account/

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult GetAccountsByOffice(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var accountFactory = new AccountFactory(conn);
                IList<Account> accounts = accountFactory.GetAccounts(id);
                return View("AccountList", accounts);
            }
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult GetAccounts(int officeId, int operId, int curId, int operSign)
        {
            using (var conn = DbConnector.Connection)
            {
                var accountFactory = new AccountFactory(conn);
                IEnumerable<Account> accounts = accountFactory.GetAccounts(officeId, operId, curId, operSign);
                return View("NlsList", accounts);

            }
        }



        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice })]
        public ActionResult Index()
        {
            using (var conn = DbConnector.Connection)
            {
                
                //IList<Office> offices = Office.GetOffices(conn, false, false, 1);
                IList<Office> offices = Office.GetOffices(conn);
                var officeList = new List<SelectListItem>{new SelectListItem{Text = "всі",Value = "0"}};
                officeList.AddRange(offices.
                                        Select(office => new SelectListItem
                                        {
                                            Selected = false,
                                            Text =
                                                string.Format("[{0}] {1}", office.Podrid,
                                                              office.OfficeName),
                                            Value = office.OfficeId.ToString()
                                        }));
                ViewData["offices"] = officeList;

                var accountFactory = new AccountFactory(conn);
                IList<Account> accounts = accountFactory.GetAccounts();
                return View(accounts);
            }
        }
        
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOfficeBranch, SiteRoles.DealerFrontOfficeBranch })]
        public ActionResult BranchAccounts()
        {
            using (var conn = DbConnector.Connection)
            {
                var accountFactory = new AccountFactory(conn);
                int officeId = WebSession.CurrentUser.ParentOfficeId;
                
                if (WebSession.CurrentUser.UserRoleIds.Contains((int)SiteRoles.MasterAdmin))
                    officeId = 0;

                IList<Account> accounts = accountFactory.GetAccounts(officeId);
                return View(accounts);
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice, SiteRoles.DealerBackOfficeBranch })]
        public ActionResult Index(int officeId)
        {
            using (var conn = DbConnector.Connection)
            {
               
           
                var accountFactory = new AccountFactory(conn);
                IList<Account> accounts = accountFactory.GetAccounts(officeId);
                return View(accounts);
            }
        }
        
       

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice })]
        public ActionResult Create()
        {
            using (var conn = DbConnector.Connection)
            {
                //IList<Office> offices = Office.GetOffices(conn, false, false, 1);
                IList<Office> offices = Office.GetOffices(conn);
                var officeList = new List<SelectListItem>();
                officeList.AddRange(offices.
                    Select(office => new SelectListItem
                    {
                        Selected = false,
                        Text = string.Format("[{0}] {1}", office.Podrid, office.OfficeName),
                        Value = office.OfficeId.ToString()
                    }));
                ViewData["offices"] = officeList;

                var currencyList = Currency.GetAllCurrencies(conn).
                    Select(currency => new SelectListItem
                    {
                        Text = string.Format("({0}) [{1}] {2}", currency.CurId, currency.CurDef, currency.CurName),
                        Value = currency.CurId.ToString()
                    }).ToList();
                ViewData["currencies"] = currencyList;

                return View();
            }
        }

        //
        // POST: /Account/Create

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice })]
        public ActionResult Create(Account a, FormCollection form)
        {
            try
            {

                using (var conn = DbConnector.Connection)
                {
                    var accountFactory = new AccountFactory(conn);
                    var account = new Account
                    {
                        Nls = a.Nls,
                        OfficeId = Convert.ToInt32(form["drOffice"]),
                        OperId = Convert.ToInt32(form["drOper"]),
                        OperSign = Convert.ToInt32(form["drOperSign"]),
                        CurId = Convert.ToInt32(form["drCurrency"])
                    };
                    accountFactory.Save(account);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

   

        public ActionResult Edit(int id)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerBackOffice})]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var conn = DbConnector.Connection)
                {
                    var accountFactory = new AccountFactory(conn);
                    Account account = accountFactory.GetAccount(id);

                    if (WebSession.CurrentUser.UserRoleIds.Contains((int)SiteRoles.MasterAdmin) 
                        || WebSession.CurrentUser.UserRoleIds.Contains((int)SiteRoles.DealerBackOffice))
                    {
                        accountFactory.Delete(account);
                    }
                    else
                    {
                        throw new Exception("CurrentUserDontHavePermissionnsToCompteThisOperation");
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
