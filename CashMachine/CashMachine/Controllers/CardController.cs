using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashMachine.Helpers;
using CashMachine.Models;
using WebMatrix.WebData;

namespace CashMachine.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        //
        // GET: /Card/

        private CardStorage _storage;

        public CardController(SiteContext context)
        {
            _storage = new CardStorage(context);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Operations()
        {
            return View();
        }

        public ActionResult Balance()
        {
            var card = _storage.GetCard(WebSession.CardId.Value);
            OperationType otype = _storage.GetOparationType(1);
            Operation operation = new Operation{Card = card,Amount = 0,OperationType = otype,Date = DateTime.Now};
            card.Operations.Add(operation);



            return View(card);
        }

        public ActionResult GetCash()
        {
            return View();
        }

    }
}
