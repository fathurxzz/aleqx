using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;

namespace Exchange.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private List<CustomMessage> _messages = new List<CustomMessage>();

        public ActionResult Index()
        {
            if (!WebSession.CurrentUser.IsAutorized)
            {
                _messages.Add(new CustomMessage
                                  {
                                      Type = MessageType.Error,
                                      Text = Helper.GetResourceString("UserNotAuthorized")
                                  });

                ViewData["messages"] = _messages;
            }
            return View(WebSession.CurrentUser);
        }

        public ActionResult EditUser(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                return View(new UserFactory(conn).GetUser(id));
            }
        }

        [HttpPost]
        public ActionResult EditUser(User u)
        {
            using (var conn = DbConnector.Connection)
            {
                UserFactory userFactory = new UserFactory(conn);
                User user = userFactory.GetUser(u.Id);

                user.Phone = u.Phone;

                userFactory.SaveChanges(user, true, false, false, false);

                WebSession.CurrentUser = user;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
