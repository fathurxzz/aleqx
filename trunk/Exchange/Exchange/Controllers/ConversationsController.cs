using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;

namespace Exchange.Controllers
{
    public class ConversationsController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();
        //
        // GET: /Conversations/

        public ActionResult Index()
        {
            using (var conn = DbConnector.Connection)
            {
                var mFactory = new MessageFactory(conn);

                var messages = mFactory.GetMessagePreviews();

                return View(messages);
            }
        }

        public ActionResult Details(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var mFactory = new MessageFactory(conn);
                var user = new UserFactory(conn).GetUser(id);
                ViewData["userName"] = user.Name;
                ViewData["officeName"] = user.OfficeName;

                var messages = mFactory.GetMessagesByUser(id).ToList();


                foreach (var message in messages.Where(m => m.UserRcv == WebSession.CurrentUser.Id || m.UserRcv == 0))
                {
                    message.ReadMessage();
                }

                if (!mFactory.Update(messages))
                {
                    _messages.Add(new CustomMessage
                    {
                        Type = MessageType.Error,
                        Text = Helper.GetResourceString("ErrorWhileUpdatingMessage") + " " + mFactory.ErrorMessage
                    });
                    ViewData["messages"] = _messages;
                    return View(messages);
                }


                return View(messages);
            }
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult SendMessage()
        {
            using (var conn = DbConnector.Connection)
            {
                var userFactory = new UserFactory(conn);
                var users = userFactory.GetUsers().Where(u => u.Id != WebSession.CurrentUser.Id).OrderBy(u => u.Podrid).ToList();

                ViewData["drRecipients"] = users.Select(u =>
                    new SelectListItem
                        {
                            Text = u.OfficeName + " " + u.Name,
                            Value = u.Id.ToString()
                        }).ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(FormCollection form)
        {

            if (string.IsNullOrEmpty(form["recipientsIds"]) && string.IsNullOrEmpty(form["officeIds"]))
            {
                _messages.Add(new CustomMessage
                {
                    Type = MessageType.Error,
                    Text = Helper.GetResourceString("PlsEnterMessageRecipients")

                });
                ViewData["messages"] = _messages;
                return View();
            }

            if (string.IsNullOrEmpty(form["taText"]))
            {
                _messages.Add(new CustomMessage
                {
                    Type = MessageType.Error,
                    Text = Helper.GetResourceString("PlsEnterMessageText")

                });
                ViewData["messages"] = _messages;
                return View();
            }


            using (var conn = DbConnector.Connection)
            {

                var messages = new List<Message>();

                if (!string.IsNullOrEmpty(form["officeIds"]))
                {
                    var officeIds = form["officeIds"].ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    messages.AddRange(officeIds.Select(id => new Message
                                                                 {
                                                                     UserId = WebSession.CurrentUser.Id,
                                                                     UserRcv = 0,
                                                                     Date = DateTime.Now,
                                                                     OfficeId = WebSession.CurrentUser.OfficeId,
                                                                     OfficeRcv = Convert.ToInt32(id),
                                                                     Text = form["taText"]
                                                                 }));
                }

                if (!string.IsNullOrEmpty(form["recipientsIds"]))
                {
                    var recepientIds = form["recipientsIds"].ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var m in recepientIds.Select(id => new Message
                                                                    {
                                                                        UserId = WebSession.CurrentUser.Id,
                                                                        UserRcv = Convert.ToInt32(id),
                                                                        Date = DateTime.Now,
                                                                        OfficeId = WebSession.CurrentUser.OfficeId,
                                                                        Text = form["taText"]
                                                                    }))
                    {
                        m.OfficeRcv = new UserFactory(conn).GetUser(m.UserRcv).OfficeId;
                        messages.Add(m);
                    }
                }

                var mFactory = new MessageFactory(conn);
                bool result = true;
                foreach (var message in messages)
                {
                    if (!mFactory.Send(message))
                    {
                        result = false;
                        _messages.Add(new CustomMessage
                        {
                            Type = MessageType.Error,
                            Text =
                                Helper.GetResourceString("ErrorWhileSendingMessage") + " " +
                                mFactory.ErrorMessage
                        });
                    }
                }
                if (!result)
                {
                    ViewData["messages"] = _messages;
                    return View();
                }
            }


            return RedirectToAction("Index");
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult GetUnreadMessages()
        {
            using (var conn = DbConnector.Connection)
            {
                var messageFactory = new MessageFactory(conn);
                IEnumerable<Message> messages = messageFactory.GetUnreadMessages().ToList();
                if (messages.Count() > 0)
                {
                    var userMessage = messages.First();
                    var message = new UserMessage
                                      {
                                          Type = MessageType.Info,
                                          Title = Helper.GetResourceString("NewMessageF") + ": " + messages.Count(),
                                          OfficeName = userMessage.OfficeName,
                                          UserName = userMessage.UserName,
                                          UserId = userMessage.UserId,
                                          Text = userMessage.Text
                                      };
                    return View("UserMessage", message);
                }
            }
            return View("CustomMessage");
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult GetRecipientsByMask(string id)
        {
            using (var conn = DbConnector.Connection)
            {
                var mFactory = new MessageFactory(conn);
                IEnumerable<Recepient> recepients = mFactory.GetRecepients(id);
                return View("Recipients", recepients);
            }
        }

    }
}
