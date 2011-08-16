using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Helpers;

namespace Exchange.Controllers
{
    public class SettingsController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();

        public ActionResult Index()
        {
            return View(WebSession.ExchangeSettings);
        }


        [HttpPost]
        public ActionResult Index(ExchangeSettings model)
        {
            string errorMessage;

            //model.CheckingCourseMorTime

            if(!ExchangeSettings.SaveSettings(model,out errorMessage))
            {
                _messages.Add(new CustomMessage
                                  {
                                      Title = Helper.GetResourceString("AnErrorWhileSavingSettings"),
                                      Text = errorMessage,
                                      Type = MessageType.Error
                                  });
            }
            else
            {
                _messages.Add(new CustomMessage
                                  {
                                      Text = Helper.GetResourceString("SettingSaved"),
                                      Type = MessageType.Info
                                  });
            }

            ViewData["messages"] = _messages;
            return View(WebSession.ExchangeSettings);
        }
    }
}
