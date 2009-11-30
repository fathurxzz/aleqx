using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using bigs.Models;

namespace bigs.Controllers
{
    public class RequestsController : BaseContentController
    {
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveRequest(string nameOfYourCompany, string yourContacts, string contactTelephone, string teleportWhereFrom, string andWhereTo, string cargoInformation)
        {
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

            return RedirectToAction("Index","Requests");
        }

    }
}
