using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CashMachine.Helpers;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class AuthController : Controller
    {
        //
        // GET: /Auth/

        private CardStorage _storage;

        public AuthController(SiteContext context)
        {
            _storage = new CardStorage(context);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("CardNumber");
        }

        

        public ActionResult CardNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(CardNotFoundException), View = "CardNotFound")]

        public ActionResult CardNumber(FormCollection form)
        {
            string formatedNumber = form["cardNumber"];
            string cardNumber = formatedNumber.Replace("-", "");

            Card card = _storage.GetCard(cardNumber);

            if (card == null)
                throw new CardNotFoundException("Карта не найдена");

            if (card.Locked)
                throw new CardNotFoundException("Данная карта заблокирована");

            WebSession.CardId = card.Id;

            return RedirectToAction("CardPin");
        }


        [HandleError(ExceptionType = typeof(CardNotFoundException), View = "CardNotFound")]
        public ActionResult CardPin()
        {
            if (WebSession.Card != null)
                return RedirectToAction("Operations", "Card");

            if (!WebSession.CardId.HasValue)
                throw new CardNotFoundException("Сначала введите номер карты");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(InvalidPinException), View = "InvalidPin")]
        [HandleError(ExceptionType = typeof(CardNotFoundException), View = "CardNotFound")]
        public ActionResult CardPin(FormCollection form)
        {
            var pin = form["cardPin"];
            Card card;
            if (!_storage.ValidatePin(WebSession.CardId.Value, pin, out card))
            {
                if (card.Locked)
                {
                    throw new CardNotFoundException("Карта заблокирована");
                }
                throw new InvalidPinException();
            }

            FormsAuthentication.SetAuthCookie(card.Number, false);
            WebSession.Card = card;

            return RedirectToAction("Operations", "Card");
        }
    }
}
