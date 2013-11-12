using System;
using System.Web.Mvc;
using System.Web.Security;
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
        [HandleError(ExceptionType = typeof(CardException), View = "CardNotFound")]
        public ActionResult CardPin(string cardNumber)
        {
            string formattedNumber = cardNumber.Replace("-", "");
            var card = _repository.Get(formattedNumber);

            return View(card);
        }
    }
}
