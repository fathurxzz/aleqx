using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;

namespace CashMachine.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private readonly IOperationRepository _repository;

        public OperationsController(IOperationRepository repository)
        {
            _repository = repository;
        }

        public ActionResult ShowBalance()
        {
            var balance = _repository.GetBalance(WebSession.CardNumber);
            return View(balance);
        }

        public ActionResult Withdraw()
        {
            return View(0M);
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(CardException), View = "OperationError")]
        public ActionResult Withdraw(decimal amount)
        {
            _repository.Withdraw(WebSession.CardNumber, amount);
            return View("Success");
        }

    }
}
