using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    [Authorize]
    [HandleError(ExceptionType = typeof(ArgumentNullException), View = "CardError")]
    public class OperationsController : Controller
    {
        private readonly IOperationRepository _repository;

        public OperationsController(IOperationRepository repository)
        {
            _repository = repository;
        }

        public ActionResult ShowBalance()
        {
            var balancePresentation = _repository.GetBalance(WebSession.GetCardNumber());
            return View(balancePresentation);
        }

        public ActionResult Withdraw()
        {
            return View(new WithdrawForm {Amount = 0});
        }

        [HttpPost]
        
        [HandleError(ExceptionType = typeof(CardException), View = "OperationError")]
        public ActionResult Withdraw(WithdrawForm withdraw)
        {
            var withdrawPresentation = _repository.Withdraw(WebSession.GetCardNumber(), withdraw.Amount);
            return View("Success", withdrawPresentation);
        }

    }
}
