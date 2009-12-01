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
        public ActionResult Index(string nameOfYourCompany, string yourContacts, string contactTelephone, string teleportWhereFrom, string andWhereTo, string cargoInformation,bool captchaValid)
        {

            if (ValidateSendRequest(captchaValid, nameOfYourCompany, yourContacts, contactTelephone, teleportWhereFrom, andWhereTo, cargoInformation))
            {

                Request request = new Request();
                request.CompanyName = nameOfYourCompany;
                request.ClientName = yourContacts;
                request.PhoneEmail = contactTelephone;
                request.TeleportFrom = teleportWhereFrom;
                request.TeleportTo = andWhereTo;
                request.CargoInfo = cargoInformation;
                request.Date = DateTime.Now;

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

                subject += "Mail from bigs.kiev.ua";

                body += "Company Name: " + nameOfYourCompany + "<br />";
                body += "Client Name: " + yourContacts + "<br />";



                ViewData["requestStatus"] = "������ ���������";


                MailRequest(body, subject);


                //return RedirectToAction("ThankYou", "Requests");
            }

            return View();
            
        }

        private bool ValidateSendRequest(bool captchaValid, string companyName, string clientName, string PhoneEmail, string TeleportFrom, string TeleportTo, string CargoInfo)
        {
            if (string.IsNullOrEmpty(companyName))
                ModelState.AddModelError("companyName", ResourcesHelper.GetResourceString("IncorrectCompanyName"));
            if (string.IsNullOrEmpty(clientName))
                ModelState.AddModelError("clientName", ResourcesHelper.GetResourceString("IncorrectClientName"));
            if (!captchaValid)
                ModelState.AddModelError("captchaInvalid", ResourcesHelper.GetResourceString("IncorrectCaptcha"));


            return ModelState.IsValid;
        }

        private void MailRequest(string body , string subject)
        {
            List<MailAddress> addresses = new List<MailAddress>();
            addresses.Add(new MailAddress(ApplicationData.DestinationEmail));
            MailHelper.SendMessage("no-reply@bigs.kiev.ua", addresses, body, subject, true);
   
        }

    }
}
