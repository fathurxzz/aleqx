using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    [Authorize]
    public class OperationsController : ApiController
    {
        private readonly IOperationRepository _repository;

        public OperationsController(IOperationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public decimal Get(string cardNumber)
        {
            return _repository.GetBalance(cardNumber);
        }

        [HttpPost]
        public HttpResponseMessage Post(WithdrawalRequest request)
        {
            try
            {
                var result = _repository.Withdraw(request.CardNumber, request.Amount);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "unknown result");
            }
            catch (CardException e)
            {
                if (e.Error == CardError.InsufficientFunds)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
                }
                throw;
            }
        }
    }
}
