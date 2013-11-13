using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Repositories;
using CashMachine.Helpers;

namespace CashMachine.Controllers
{
    public class CardApiController : ApiController
    {
        private readonly ICardRepository _repository;

        public CardApiController(ICardRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage Get(string cardNumber)
        {
            string formattedNumber = cardNumber.Replace("-", "");

            try
            {
                var card = _repository.Get(formattedNumber);

                

                return Request.CreateResponse(HttpStatusCode.OK, card);
            }
            catch (CardException e)
            {
                switch (e.Error)
                {
                    case CardError.Unknown:
                        break;
                    case CardError.NotFound:
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, e);
                    case CardError.Locked:
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
                }
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(Card card)
        {
            try
            {
                var result =_repository.Validate(card.Number, card.Pin);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(card.Number, false);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            catch (ValidationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
            }
            
        }

    }
}
