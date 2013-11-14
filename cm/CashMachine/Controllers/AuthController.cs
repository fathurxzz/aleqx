using System;
using System.Web.Mvc;
using System.Web.Security;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;

namespace CashMachine.Controllers
{
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class AuthController : Controller
    {
        //
        // GET: /Auth/
        private readonly ICardRepository _repository;

        public AuthController(ICardRepository repository)
        {
            _repository = repository;
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
        [HandleError(ExceptionType = typeof(CardException), View = "CardError")]

        public ActionResult CardNumber(string cardNumber)
        {
            string formattedNumber = cardNumber.Replace("-", "");
            var card = _repository.GetCard(formattedNumber);
            return View("CardPin", card);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(ValidationException), View = "InvalidPin")]
        [HandleError(ExceptionType = typeof(CardException), View = "CardError")]
        public ActionResult CardPin(Card card)
        {
            if (_repository.Validate(card.Number, card.Pin))
            {
                FormsAuthentication.SetAuthCookie(card.Number, false);
                WebSession.SetCartNumber(card.Number);
            }
            return RedirectToAction("Index", "Card");
        }
    }
}
