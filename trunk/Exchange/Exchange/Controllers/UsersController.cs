using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;

namespace Exchange.Controllers
{
    public class UsersController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminCurrencyControler, SiteRoles.AdminFrontOfficeBranch, SiteRoles.AdminBackOffice })]
        public ActionResult Index(string podrid, string username)
        {
            WebSession.SetValues(
                new FilterValuesUpdater { Podrid = podrid,UserName = username},
                out _messages);

            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                IEnumerable<UserPresentation> users = userFactory.GetUsers();
                return View(users.Where(u => (u.Podrid.Contains(WebSession.Podrid) || WebSession.Podrid == "")&&(u.Name.ToLower().Contains(WebSession.UserName.ToLower())||WebSession.UserName=="")));
            }
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminCurrencyControler, SiteRoles.AdminFrontOfficeBranch, SiteRoles.AdminBackOffice })]
        public ActionResult EditPermissions(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                IEnumerable<UserRole> userRolesToAssign = UserRole.GetRolesByCurrentUserPermissions(WebSession.CurrentUser, conn);
                ViewData["userRolesToAssign"] = userRolesToAssign;
                var currecies = Currency.GetAllCurrenciesAccordingAccounts(conn);
                ViewData["allCurrencies"] = currecies;
                

                var userFactory = new UserFactory(conn);
                User user = userFactory.GetUser(id);

                var offices= Office.GetOffices(conn, false, true).Where(o=>o.Podrid==user.Podrid).ToList();


                List<SelectListItem> selectedOffices = offices.Select(
                    o => new SelectListItem
                    {
                        Text = o.OfficeName.ToString(),
                        Value = o.OfficeId.ToString(),
                        Selected = user.OfficeId==o.OfficeId
                    }).ToList();

                ViewData["changeOffices"] = selectedOffices;
                return View(user);
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch, SiteRoles.AdminCurrencyControler, SiteRoles.AdminBackOffice })]
        public ActionResult EditPermissions(User u, FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                PostData postDataRermissions = form.ProcessPostData("cbur");

                var userFactory = new UserFactory(conn);
                User user = userFactory.GetUser(u.Id);

                user.IsAutorized = u.IsAutorized;
                user.Phone = u.Phone;
                user.UserIdOdb = u.UserIdOdb;

                foreach (var item in postDataRermissions)
                {
                    int key = Convert.ToInt32(item.Key);
                    if (Convert.ToBoolean(item.Value))
                    {
                        if (!user.UserRoleIds.Contains(key))
                        {
                            user.UserRoleIds.Add(key);
                        }
                    }
                    else
                    {
                        if (user.UserRoleIds.Contains(key))
                        {
                            user.UserRoleIds.Remove(key);
                        }
                    }
                }


                PostData postDataCurrencyView = form.ProcessPostData("cbcv");
                PostData postDataCurrencyProcess = form.ProcessPostData("cbcp");

                user.CurrencyList.Clear();
                foreach (var itemView in postDataCurrencyView.Where(item => Convert.ToBoolean(item.Value)))
                {
                    bool process = false;
                    foreach (var itemProcess in postDataCurrencyProcess.Where(item => Convert.ToBoolean(item.Value)))
                    {
                        if (itemView.Key == itemProcess.Key)
                        {
                            process = true;
                        }
                    }
                    user.CurrencyList.Add(new UserCurrency { AllowedProcessing = process, CurId = Convert.ToInt32(itemView.Key) });
                }

                user.OfficeId = Convert.ToInt32(form["drOffices"]);

                userFactory.SaveChanges(user);
            }
            return RedirectToAction("Index");
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion, SiteRoles.AdminFrontOffice, SiteRoles.AdminFrontOfficeBranch })]
        public ActionResult Details(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                User user = userFactory.GetUser(id);
                return View(user);
            }
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice })]
        public ActionResult Moderators()
        {
            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                IList<Moderator> moderators = userFactory.GetModerators();
                return View(moderators);
            }
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice })]
        public ActionResult EditModeratorsOffices(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                var user = userFactory.GetUser(id);

                ViewData["userId"] = user.Id;

                IList<Office> offices = Office.GetOffices(conn, false, true, 1);
                var moderatorsOffices = offices.Select(
                    office => new OfficeSelectListItem
                                  {
                                      Text = "(" + office.Podrid + ") " + office.OfficeName,
                                      Value = office.OfficeId.ToString(),
                                      Selected = user.ModeratorOfficeIdList.Contains(office.OfficeId),
                                      OfficeLevel = office.OfficeLevel
                                  }).ToList();

                return View(moderatorsOffices);
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice })]
        public ActionResult EditModeratorsOffices(FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                int userId = Convert.ToInt32(form["userId"]);
                var userFactory = new UserFactory(conn);
                var user = userFactory.GetUser(userId);

                user.ModeratorOfficeIdList.Clear();

                var postData = form.ProcessPostData("cb");
                foreach (var item in postData.Where(item => Convert.ToBoolean(item.Value)))
                {
                    user.ModeratorOfficeIdList.Add(Convert.ToInt32(item.Key));
                }

                userFactory.SaveChanges(user, false, false, false, true);

                return RedirectToAction("Moderators");
            }
        }


        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice })]
        public ActionResult ViewModeratorsOffices(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                var user = userFactory.GetUser(id);

                IList<Office> offices = Office.GetOffices(conn, false, true, 1);
                offices = offices.Where(o => o.OfficeId.In(user.ModeratorOfficeIdList.ToArray())).ToList();
                return View(offices);
            }
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.AdminFrontOffice })]
        public ActionResult DeleteModeratorsOffices(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                var user = userFactory.GetUser(id);

                user.ModeratorOfficeIdList.Clear();
                userFactory.SaveChanges(user, false, false, false, true);

                return RedirectToAction("Moderators");
            }
        }

        public ActionResult Login(FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {

                int userId = string.IsNullOrEmpty(form["drUserList"]) ? 0 : Convert.ToInt32(form["drUserList"]);

                if(userId!=0)
                {
                    User user = new User(userId);
                    Session["user"] = user;
                    return RedirectToAction("Index", "Home");
                }

                var userFactory = new UserFactory(conn);
                List<UserPresentation> users = userFactory.GetUsers(1).ToList();


                var userList =
                    users.OrderBy(u => u.Podrid + u.OfficeId).Select(
                    u => new SelectListItem
                             {
                                 Text = u.Podrid + " - " + u.OfficeName + " - " + u.Name,
                                 Value = u.Id.ToString()
                             }).
                        ToList();
                ViewData["userList"] = userList;
            }

            return View();
        }
    }
}
