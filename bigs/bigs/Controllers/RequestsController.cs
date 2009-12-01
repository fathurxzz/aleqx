using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;
using bigs.Helpers;
using System.Configuration;
using System.Net.Mail;

namespace bigs.Controllers
{
    public class RequestsController : BaseContentController
    {

        [AcceptVerbs(HttpVerbs.Post)]
        [CaptchaValidation("captcha")]
        public ActionResult SendRequest(string nameOfYourCompany, string yourContacts, string contactTelephone, string teleportWhereFrom, string andWhereTo, string cargoInformation,bool captchaValid)
        {

            if (ValidateSendRequest(captchaValid, nameOfYourCompany, yourContacts))
            {

                Request request = new Request();
                request.CompanyName = nameOfYourCompany;
                request.ClientName = yourContacts;
                request.PhoneEmail = contactTelephone;
                request.TeleportFrom = teleportWhereFrom;
                request.TeleportTo = andWhereTo;
                request.CargoInfo = cargoInformation;
                request.Date = DateTime.Now;
                MailRequest(request);

                /*
                using (DataStorage context = new DataStorage())
                {
                    Request request = new Request();
                    request.CompanyName = nameOfYourCompany;
                    request.ClientName = yourContacts;
                    request.PhoneEmail = contactTelephone;
                    request.TeleportFrom = teleportWhereFrom;
                    request.TeleportTo = andWhereTo;
                    request.CargoInfo = cargoInformation;
                    request.Date = DateTime.Now;
                    context.AddToRequest(request);
                    context.SaveChanges();
                }
                 * */

                string body = string.Empty;
                string subject = string.Empty;
                List<MailAddress> addresses = new List<MailAddress>();
                addresses.Add(new MailAddress(ConfigurationManager.AppSettings["MailAddress"]));
                MailHelper.SendMessage(ConfigurationManager.AppSettings["FeedbackEmail"], addresses, body, subject, false);

            }


            return RedirectToAction("Index", "Requests");
            
        }

        private bool ValidateSendRequest(bool captchaValid, string companyName, string clientName)
        {
            if (string.IsNullOrEmpty(companyName))
                ModelState.AddModelError("companyName", ResourcesHelper.GetResourceString("IncorrectCompanyName"));
            if (string.IsNullOrEmpty(clientName))
                ModelState.AddModelError("clientName", ResourcesHelper.GetResourceString("IncorrectClientName"));
            if (!captchaValid)
                ModelState.AddModelError("captchaInvalid", ResourcesHelper.GetResourceString("IncorrectCaptcha"));

            return ModelState.IsValid;
        }

        private void MailRequest(Request request)
        {
            
        }

    }
}
